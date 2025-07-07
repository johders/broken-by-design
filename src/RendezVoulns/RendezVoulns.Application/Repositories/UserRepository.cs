using Dapper;
using Npgsql;
using RendezVoulns.Application.Common.Errors;
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
                "users_username_active_idx" => new DuplicateException(Errors.Users.DuplicateUsernameErrorCode, Messages.Users.DuplicateUsername),
                "users_email_active_idx" => new DuplicateException(Errors.Users.DuplicateEmailErrorCode, Messages.Users.DuplicateEmail),
                "users_slug_active_idx" => new DuplicateException(Errors.Users.DuplicateSlugErrorCode, Messages.Users.DuplicateSlug),
                _ => new DuplicateException(Errors.Users.DuplicateErrorCode, Messages.Users.Duplicate)
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
                "users_username_active_idx" => new DuplicateException(Errors.Users.DuplicateUsernameErrorCode, Messages.Users.DuplicateUsername),
                "users_email_active_idx" => new DuplicateException(Errors.Users.DuplicateEmailErrorCode, Messages.Users.DuplicateEmail),
                "users_slug_active_idx" => new DuplicateException(Errors.Users.DuplicateSlugErrorCode, Messages.Users.DuplicateSlug),
                _ => new DuplicateException(Errors.Users.DuplicateErrorCode, Messages.Users.Duplicate)
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

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = """
            SELECT 1 
            FROM users
            WHERE id = @Id
            AND deleted_on IS NULL
            LIMIT 1;
            """;

        var result = await connection.QueryFirstOrDefaultAsync<int?>(new CommandDefinition(sql, new { Id = id }, cancellationToken: token));

        return result.HasValue;
    }

    public async Task<bool> UsernameExistsAsync(string username, Guid? excludeId = null, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = """
            SELECT 1 
            FROM users
            WHERE username = @Username
            AND (@ExcludeId IS NULL OR id != @ExcludeId)
            AND deleted_on IS NULL
            LIMIT 1;
            """;

        var result = await connection.QueryFirstOrDefaultAsync<int?>(new CommandDefinition(sql, new { Username = username, ExcludeId = excludeId }, cancellationToken: token));

        return result.HasValue;
    }

    public async Task<bool> EmailExistsAsync(string email, Guid? excludeId = null, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = """
            SELECT 1 
            FROM users
            WHERE email = @Email
            AND (@ExcludeId IS NULL OR id != @ExcludeId)
            AND deleted_on IS NULL
            LIMIT 1;
            """;

        var result = await connection.QueryFirstOrDefaultAsync<int?>(new CommandDefinition(sql, new { Email = email, ExcludeId = excludeId }, cancellationToken: token));

        return result.HasValue;
    }

    public async Task<bool> SlugExistsAsync(string slug, Guid? excludeId = null, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = """
            SELECT 1 
            FROM users
            WHERE slug = @Slug
            AND (@ExcludeId IS NULL OR id != @ExcludeId)
            AND deleted_on IS NULL
            LIMIT 1;
            """;

        var result = await connection.QueryFirstOrDefaultAsync<int?>(new CommandDefinition(sql, new { Slug = slug, ExcludeId = excludeId }, cancellationToken: token));

        return result.HasValue;
    }
}