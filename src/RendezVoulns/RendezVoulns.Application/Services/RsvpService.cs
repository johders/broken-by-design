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
    
    public Task<Result> DeleteRsvpAsync(Guid eventId, Guid userId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<Rsvp>>> GetRsvpsForUserAsync(Guid userId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}