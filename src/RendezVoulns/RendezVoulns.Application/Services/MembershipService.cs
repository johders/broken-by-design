using RendezVoulns.Application.Common.Errors;
using RendezVoulns.Application.Common.Results;
using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Application.Repositories.Interfaces;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Application.Services;

public class MembershipService(IMembershipRepository membershipRepository) : IMembershipService
{
    private readonly IMembershipRepository _membershipRepository = membershipRepository;

    public async Task<Result> JoinAsync(Guid userId, Guid groupId, CancellationToken token = default)
    {
        var joined = await _membershipRepository.JoinAsync(userId, groupId, token);

        return joined
            ? Result.Success()
            : Result.Failure(Errors.Memberships.CreateFailedError);
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

    public Task<Result> UpdateAsync(Membership membership, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}
