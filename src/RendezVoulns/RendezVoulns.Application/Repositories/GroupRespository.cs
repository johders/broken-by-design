using Dapper;
using Npgsql;
using RendezVoulns.Application.Errors.Postgresql;
using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Application.Persistence.Database;
using RendezVoulns.Application.Repositories.Interfaces;

namespace RendezVoulns.Application.Repositories;

public class GroupRepository(IDbConnectionFactory dbConnectionFactory) : IGroupRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task<bool> CreateAsync(Group group, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var sql = """
            INSERT INTO groups (id, name, description, created_on)
            VALUES (@Id, @Name, @Description, @CreatedOn);
            """;

        try
        {
            var result = await connection.ExecuteAsync(new CommandDefinition(sql, group, transaction, cancellationToken: token));

            transaction.Commit();
            return result > 0;
        }
        catch (PostgresException ex) when (ex.SqlState == Npgsql.PostgresErrorCodes.UniqueViolation)
        {
            throw new DuplicateException("A group with this name already exists");
        }
    }

    public async Task<Group?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = """
            SELECT id, name, description AS Description, created_on AS CreatedOn FROM groups
            WHERE id = @Id
            AND deleted_on IS NULL
            """;
        var group = await connection.QueryFirstOrDefaultAsync<Group>(new CommandDefinition(sql, new { Id = id }, cancellationToken: token));

        return group;
    }

    public async Task<IEnumerable<Group>> GetAllAsync(CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = """
            SELECT id, name, description AS Description, created_on AS CreatedOn FROM groups
            WHERE deleted_on IS NULL
            """;
        var groups = await connection.QueryAsync<Group>(new CommandDefinition(sql, cancellationToken: token));

        return groups;
    }

    public async Task<bool> UpdateAsync(Group group, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var transaction = connection.BeginTransaction();

        var sql = """
            UPDATE groups SET name = @Name, description = @Description, updated_on = @UpdatedOn
            WHERE id = @id;
            """;

        try
        {
            var result = await connection.ExecuteAsync(new CommandDefinition(sql, group, transaction, cancellationToken: token));

            transaction.Commit();
            return result > 0;
        }
        catch (PostgresException ex) when (ex.SqlState == Npgsql.PostgresErrorCodes.UniqueViolation)
        {
            throw new DuplicateException("A group with this name already exists");
        }
    }
    public async Task<bool> SoftDeleteAsync(Guid id, DateTimeOffset deletedOn, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var sql = """
            UPDATE groups SET updated_on = @DeletedOn, deleted_on = @DeletedOn
            WHERE id = @id;
            """;

        var result = await connection.ExecuteAsync(new CommandDefinition(sql, new { Id = id, DeletedOn = deletedOn }, transaction, cancellationToken: token));

        transaction.Commit();
        return result > 0;
    }


    public Task<bool> ExistsByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}