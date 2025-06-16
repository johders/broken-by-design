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

    public Task<bool> ExistsByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Tag>> GetAllAsync(CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Tag?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Tag?> GetBySlugAsync(string slug, CancellationToken token = default)
    {
        throw new NotImplementedException();
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