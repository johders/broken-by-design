using RendezVoulns.Application.Common.Errors;
using RendezVoulns.Application.Common.Results;
using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Application.Repositories.Interfaces;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Application.Services;

public class RsvpService(IRsvpRepository rsvpRepository) : IRsvpService
{
    private readonly IRsvpRepository _rsvpRepository = rsvpRepository;
    public async Task<Result> RsvpEventAsync(Rsvp rsvp, CancellationToken token = default)
    {
        var created = await _rsvpRepository.RsvpEventAsync(rsvp, token);

        return created
            ? Result.Success()
            : Result.Failure(Errors.Rsvps.CreateFailedError);
    }
    
    public async Task<Result<Rsvp?>> GetByIdAsync(Guid id, Guid userId, CancellationToken token = default)
    {
        var rsvp = await _rsvpRepository.GetByIdAsync(id, userId, token);
        return rsvp is not null
            ? Result<Rsvp?>.Success(rsvp)
            : Result<Rsvp?>.Failure(Errors.Rsvps.NotFoundError);
    }

    public async Task<Result> SoftDeleteAsync(Guid eventId, Guid userId, CancellationToken token = default)
    {
        var deleted = await _rsvpRepository.SoftDeleteAsync(eventId, userId, token);

        return deleted
            ? Result.Success()
            : Result.Failure(Errors.Rsvps.DeleteFailedError);
    }

    public async Task<Result<IEnumerable<Rsvp>>> GetRsvpsForUserAsync(Guid userId, CancellationToken token = default)
    {
        var rsvps = await _rsvpRepository.GetRsvpsForUserAsync(userId, token);
        return Result<IEnumerable<Rsvp>>.Success(rsvps);
    }
}