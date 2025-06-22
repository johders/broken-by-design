using RendezVoulns.Application.Models.Entities;

namespace RendezVoulns.Application.Repositories.Interfaces;

public interface IRsvpRepository
{
    Task<bool> RsvpEventAsync(Rsvp rsvp, CancellationToken token = default);
    Task<bool> SoftDeleteAsync(Guid eventId, Guid userId, CancellationToken token = default);
    Task<IEnumerable<Rsvp>> GetRsvpsForUserAsync(Guid userId, CancellationToken token = default);
    Task<Rsvp?> GetByIdAsync(Guid id, Guid userId, CancellationToken token = default);
}