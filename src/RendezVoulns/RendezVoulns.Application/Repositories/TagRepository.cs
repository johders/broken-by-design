using Dapper;
using Npgsql;
using RendezVoulns.Application.Common.Errors;
using RendezVoulns.Application.Common.Exceptions;
using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Application.Persistence.Database;
using RendezVoulns.Application.Repositories.Interfaces;

namespace RendezVoulns.Application.Repositories;

public class TagRepository(IDbConnectionFactory dbConnectionFactory) : ITagRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
    public async Task<bool> CreateAsync(Tag tag, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var sql = """
            INSERT INTO tags (id, name, color_hex, created_on)
            VALUES (@Id, @Name, @ColorHex, @CreatedOn);
            """;

        try
        {
            var result = await connection.ExecuteAsync(new CommandDefinition(sql, tag, transaction, cancellationToken: token));

            transaction.Commit();
            return result > 0;
        }
        catch (PostgresException ex) when (ex.SqlState == Npgsql.PostgresErrorCodes.UniqueViolation)
        {
            throw new DuplicateException(Errors.Tags.DuplicateNameErrorCode, Messages.Tags.DuplicateName);
        }
    }

    public async Task<Tag?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = """
            SELECT id, name, color_hex AS ColorHex, created_on AS CreatedOn FROM tags
            WHERE id = @Id
            AND deleted_on IS NULL
            """;
        var tag = await connection.QueryFirstOrDefaultAsync<Tag>(new CommandDefinition(sql, new { Id = id }, cancellationToken: token));

        return tag;
    }

    public async Task<IEnumerable<Tag>> GetAllAsync(CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = """
            SELECT id, name, color_hex AS ColorHex, created_on AS CreateOn FROM tags
            WHERE deleted_on IS NULL
            """;
        var tags = await connection.QueryAsync<Tag>(new CommandDefinition(sql, cancellationToken: token));

        return tags;
    }

    public async Task<bool> UpdateAsync(Tag tag, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var transaction = connection.BeginTransaction();

        var sql = """
            UPDATE tags SET name = @Name, color_hex = @ColorHex, updated_on = @UpdatedOn
            WHERE id = @id;
            """;

        try
        {
            var result = await connection.ExecuteAsync(new CommandDefinition(sql, tag, transaction, cancellationToken: token));

            transaction.Commit();
            return result > 0;
        }
        catch (PostgresException ex) when (ex.SqlState == Npgsql.PostgresErrorCodes.UniqueViolation)
        {
            throw new DuplicateException(Errors.Tags.DuplicateNameErrorCode, Messages.Tags.DuplicateName);
        }
    }

    public async Task<bool> SoftDeleteAsync(Guid id, DateTimeOffset deletedOn, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var sql = """
            UPDATE tags SET updated_on = @DeletedOn, deleted_on = @DeletedOn
            WHERE id = @id;
            """;

        var result = await connection.ExecuteAsync(new CommandDefinition(sql, new { Id = id, DeletedOn = deletedOn }, transaction, cancellationToken: token));

        transaction.Commit();
        return result > 0;
    }

    public async Task<bool> DeleteFromEventAsync(Guid eventId, Guid tagId, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var sql = """
                DELETE FROM event_tags
                WHERE event_id = @EventId AND tag_id = @TagId;
                """;

        var result = await connection.ExecuteAsync(new CommandDefinition(sql, new { EventId = eventId, TagId = tagId }, transaction, cancellationToken: token));

        transaction.Commit();
        return result > 0;
    }

    public async Task<bool> TagEventAsync(Guid eventId, Guid tagId, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var sql = """
                INSERT INTO event_tags (event_id, tag_id)
                VALUES (@EventId, @TagId)
                ON CONFLICT DO NOTHING;
                """;

        var result = await connection.ExecuteAsync(
            new CommandDefinition(sql, new { EventId = eventId, TagId = tagId }, transaction, cancellationToken: token));

        transaction.Commit();
        return result > 0;
    }
}