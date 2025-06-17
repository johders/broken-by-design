using Dapper;
using Npgsql;
using RendezVoulns.Application.Errors.Postgresql;
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
                "users_username_key" => new DuplicateException("A user with this username already exists"),
                "users_email_key" => new DuplicateException("A user with this email already exists"),
                "events_slug_active_idx" => new DuplicateException("A user with this slug already exists"),
                _ => new DuplicateException("A user with these details already exists.")
            };
        }
    }

    public Task<bool> ExistsByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllAsync(CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetBySlugAsync(string slug, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SoftDeleteAsync(Guid id, DateTimeOffset deletedOn, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(User user, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}