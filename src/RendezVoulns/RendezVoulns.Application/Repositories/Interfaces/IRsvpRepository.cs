using RendezVoulns.Application.Models.Entities;

namespace RendezVoulns.Application.Repositories.Interfaces;

public interface IRsvpRepository
{
    Task<bool> RsvpEventAsync(Guid eventId, string status, Guid userId, CancellationToken token = default);
    Task<bool> DeleteRsvpAsync(Guid eventId, Guid userId, CancellationToken token = default);
    Task<IEnumerable<Rsvp>> GetRsvpsForUserAsync(Guid userId, CancellationToken token = default);
}