using RendezVoulns.Application.Common.Results;
using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Application.Repositories.Interfaces;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Application.Services;

public class MembershipService(IMembershipRepository membershipRepository) : IMembershipService
{
    private readonly IMembershipRepository _membershipRepository = membershipRepository;
    public Task<Result> CreateAsync(Membership membership, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Rsvp?>> GetByIdAsync(Guid id, Guid userId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<Rsvp>>> GetUserMembershipsAsync(Guid userId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> SoftDeleteAsync(Guid userId, Guid groupId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}
