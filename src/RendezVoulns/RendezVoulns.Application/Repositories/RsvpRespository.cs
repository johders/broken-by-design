using Dapper;
using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Application.Persistence.Database;
using RendezVoulns.Application.Repositories.Interfaces;

namespace RendezVoulns.Application.Repositories;

public class RsvpRepository(IDbConnectionFactory dbConnectionFactory) : IRsvpRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task<bool> RsvpEventAsync(Rsvp rsvp, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var sql = """
                INSERT INTO rsvps (user_id, event_id, status, responded_on)
                VALUES (@UserId, @EventId, @Status, @RespondedOn)
                ON CONFLICT (user_id, event_id) DO UPDATE
                SET status = EXCLUDED.status, responded_on = EXCLUDED.responded_on, 
                updated_on = NOW(), deleted_on = NULL
                """;
        var result = await connection.ExecuteAsync(new CommandDefinition(sql, new { rsvp.UserId, rsvp.EventId, Status = rsvp.Status.ToString(), rsvp.RespondedOn }, cancellationToken: token));

        transaction.Commit();
        return result > 0;
    }

    public async Task<Rsvp?> GetByIdAsync(Guid id, Guid userId, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = """
            SELECT user_id AS UserId, event_id AS EventId, status, responded_on AS RespondedOn, updated_on AS UpdatedOn FROM rsvps
            WHERE event_id = @Id
            AND user_id = @UserId
            AND deleted_on IS NULL
            """;
        var rsvps = await connection.QueryFirstOrDefaultAsync<Rsvp>(new CommandDefinition(sql, new { Id = id, UserId = userId }, cancellationToken: token));

        return rsvps;
    }

    public async Task<bool> SoftDeleteAsync(Guid eventId, Guid userId, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var sql = """
            UPDATE rsvps SET updated_on = NOW(), deleted_on = NOW()
            WHERE event_id = @EventId
            AND user_id = @UserId;
            """;

        var result = await connection.ExecuteAsync(new CommandDefinition(sql, new { EventId = eventId, UserId = userId }, transaction, cancellationToken: token));

        transaction.Commit();
        return result > 0;
    }

    public async Task<IEnumerable<Rsvp>> GetRsvpsForUserAsync(Guid userId, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = """
            SELECT user_id AS UserId, event_id AS EventId, status, responded_on AS RespondedOn FROM rsvps
            WHERE deleted_on IS NULL
            AND user_id = @UserId
            """;
        var rsvps = await connection.QueryAsync<Rsvp>(new CommandDefinition(sql, new { UserId = userId }, cancellationToken: token));

        return rsvps;
    }
}