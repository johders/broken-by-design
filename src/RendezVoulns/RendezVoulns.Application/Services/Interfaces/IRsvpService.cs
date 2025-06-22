using RendezVoulns.Application.Common.Results;
using RendezVoulns.Application.Models.Entities;

namespace RendezVoulns.Application.Services.Interfaces;

public interface IRsvpService
{ 
    Task<Result> RsvpEventAsync(Guid eventId, string status, Guid userId, CancellationToken token = default);
    Task<Result> DeleteRsvpAsync(Guid eventId, Guid userId, CancellationToken token = default);
    Task<Result<IEnumerable<Rsvp>>> GetRsvpsForUserAsync(Guid userId, CancellationToken token = default);
}