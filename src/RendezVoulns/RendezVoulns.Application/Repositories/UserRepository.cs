using Dapper;
using Npgsql;
using RendezVoulns.Application.Common.Exceptions;
using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Application.Persistence.Database;
using RendezVoulns.Application.Repositories.Interfaces;

namespace RendezVoulns.Application.Repositories;

public class UserRepository(IDbConnectionFactory dbConnectionFactory) : IUserRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task<bool> CreateAsync(User user, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var sql = """
            INSERT INTO users (id, username, email, profile_image_url, slug, created_on)
            VALUES (@Id, @Username, @Email, @ProfileImageUrl, @Slug, @CreatedOn)
            """;

        try
        {
            var result = await connection.ExecuteAsync(new CommandDefinition(sql, user, transaction, cancellationToken: token));

            transaction.Commit();
            return result > 0;
        }
        catch (PostgresException ex) when (ex.SqlState == Npgsql.PostgresErrorCodes.UniqueViolation)
        {
            throw ex.ConstraintName switch
            {
                "users_username_active_idx" => new DuplicateException("TEMP", "A user with this username already exists"),
                "users_email_active_idx" => new DuplicateException("TEMP", "A user with this email already exists"),
                "users_slug_active_idx" => new DuplicateException("TEMP", "A user with this slug already exists"),
                _ => new DuplicateException("TEMP", "A user with these details already exists.")
            };
        }
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = """
                SELECT id, username, email, profile_image_url AS ProfileImageUrl, slug, created_on AS CreatedOn FROM users
                WHERE id = @Id
                AND deleted_on IS NULL;
                """;

        var user = await connection.QueryFirstOrDefaultAsync<User>(new CommandDefinition(sql, new { Id = id }, cancellationToken: token));

        return user;
    }

    public async Task<User?> GetBySlugAsync(string slug, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = """
                SELECT id, username, email, profile_image_url AS ProfileImageUrl, slug, created_on AS CreatedOn FROM users
                WHERE slug = @Slug
                AND deleted_on IS NULL;
                """;

        var user = await connection.QueryFirstOrDefaultAsync<User>(new CommandDefinition(sql, new { Slug = slug }, cancellationToken: token));

        return user;
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = """
            SELECT id, username, email, profile_image_url AS ProfileImageUrl, slug, created_on AS CreatedOn FROM users
            WHERE deleted_on IS NULL;
            """;
        var users = await connection.QueryAsync<User>(new CommandDefinition(sql, cancellationToken: token));

        return users;
    }

    public async Task<bool> UpdateAsync(User user, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var transaction = connection.BeginTransaction();

        var sql = """
            UPDATE users SET username = @Username, email = @Email, profile_image_url = @ProfileImageUrl, slug = @Slug, updated_on = @UpdatedOn
            WHERE id = @id;
            """;

        try
        {
            var result = await connection.ExecuteAsync(new CommandDefinition(sql, user, transaction, cancellationToken: token));

            transaction.Commit();
            return result > 0;
        }
        catch (PostgresException ex) when (ex.SqlState == Npgsql.PostgresErrorCodes.UniqueViolation)
        {
            throw ex.ConstraintName switch
            {
                "users_username_active_idx" => new DuplicateException("TEMP", "A user with this username already exists"),
                "users_email_active_idx" => new DuplicateException("TEMP", "A user with this email already exists"),
                "users_slug_active_idx" => new DuplicateException("TEMP", "A user with this slug already exists"),
                _ => new DuplicateException("TEMP", "A user with these details already exists.")
            };
        }
    }

    public async Task<bool> SoftDeleteAsync(Guid id, DateTimeOffset deletedOn, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var sql = """
            UPDATE users SET updated_on = @DeletedOn, deleted_on = @DeletedOn
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