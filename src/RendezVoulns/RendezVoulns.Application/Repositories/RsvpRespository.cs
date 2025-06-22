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

    public Task<bool> DeleteRsvpAsync(Guid eventId, Guid userId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Rsvp>> GetRsvpsForUserAsync(Guid userId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}