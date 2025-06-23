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
    
    public async Task<Result<Membership?>> GetByIdAsync(Guid userId, Guid groupId, CancellationToken token = default)
    {
        var membership = await _membershipRepository.GetByIdAsync(userId, groupId, token);
        return membership is not null
            ? Result<Membership?>.Success(membership)
            : Result<Membership?>.Failure(Errors.Memberships.NotFoundError);
    }

    public async Task<Result<IEnumerable<Membership>>> GetUserMembershipsAsync(Guid userId, CancellationToken token = default)
    {
        var memberships = await _membershipRepository.GetUserMembershipsAsync(userId, token);
        return Result<IEnumerable<Membership>>.Success(memberships);
    }

    public async Task<Result> SoftDeleteAsync(Guid userId, Guid groupId, CancellationToken token = default)
    {
        var deleted = await _membershipRepository.SoftDeleteAsync(userId, groupId, token);

        return deleted
            ? Result.Success()
            : Result.Failure(Errors.Memberships.DeleteFailedError);
    }

    public async Task<Result<Membership>> UpdateAsync(Membership membership, CancellationToken token = default)
    {
        var updated = await _membershipRepository.UpdateAsync(membership, token);

        return updated
            ? Result<Membership>.Success(membership)
            : Result<Membership>.Failure(Errors.Memberships.UpdateFailedError);
    }
}
