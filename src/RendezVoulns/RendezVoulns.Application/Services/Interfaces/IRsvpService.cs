using RendezVoulns.Application.Common.Results;
using RendezVoulns.Application.Models.Entities;

namespace RendezVoulns.Application.Services.Interfaces;

public interface IRsvpService
{
    Task<Result> RsvpEventAsync(Rsvp rsvp, CancellationToken token = default);
    Task<Result> SoftDeleteAsync(Guid eventId, Guid userId, CancellationToken token = default);
    Task<Result<IEnumerable<Rsvp>>> GetRsvpsForUserAsync(Guid userId, CancellationToken token = default);    
    Task<Result<Rsvp?>> GetByIdAsync(Guid id, Guid userId, CancellationToken token = default);
}