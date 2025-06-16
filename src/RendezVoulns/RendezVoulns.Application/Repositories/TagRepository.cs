using Dapper;
using Npgsql;
using RendezVoulns.Application.Errors.Postgresql;
using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Application.Persistence.Database;
using RendezVoulns.Application.Repositories.Interfaces;

namespace RendezVoulns.Application.Repositories;

public class TagRepository(IDbConnectionFactory dbConnectionFactory) : ITagRepository
{
    private readonly IDbConnectionFactory _dbConnectionfactory = dbConnectionFactory;
    public async Task<bool> CreateAsync(Tag tag, CancellationToken token = default)
    {
        using var connection = await _dbConnectionfactory.CreateConnectionAsync();
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
            throw new DuplicateException("A tag with this name already exists");
        }
    }

    public async Task<Tag?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionfactory.CreateConnectionAsync();

        var sql = """
        SELECT id, name, color_hex AS ColorHex, created_on AS CreateOn FROM tags
        WHERE id = @Id
        AND deleted_on IS NULL
        """;
        var tag = await connection.QueryFirstOrDefaultAsync<Tag>(new CommandDefinition(sql, new { Id = id }, cancellationToken: token));

        return tag;
    }

    public async Task<IEnumerable<Tag>> GetAllAsync(CancellationToken token = default)
    {
        using var connection = await _dbConnectionfactory.CreateConnectionAsync();

        var sql = """
        SELECT id, name, color_hex AS ColorHex, created_on AS CreateOn FROM tags
        WHERE deleted_on IS NULL
        """;
        var tags = await connection.QueryAsync<Tag>(new CommandDefinition(sql, cancellationToken: token));

        return tags;
    }



    public Task<bool> SoftDeleteAsync(Guid id, DateTimeOffset deletedOn, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Tag appEvent, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}