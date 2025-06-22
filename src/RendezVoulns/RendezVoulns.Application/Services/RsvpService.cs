using RendezVoulns.Application.Common.Results;
using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Application.Repositories.Interfaces;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Application.Services;

public class RsvpService(IRsvpRepository rsvpRepository) : IRsvpService
{
    private readonly IRsvpRepository _rsvpRepository = rsvpRepository;
    public Task<Result> DeleteRsvpAsync(Guid eventId, Guid userId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<Rsvp>>> GetRsvpsForUserAsync(Guid userId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> RsvpEventAsync(Guid eventId, string status, Guid userId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}