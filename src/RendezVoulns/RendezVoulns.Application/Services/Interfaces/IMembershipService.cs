using RendezVoulns.Application.Common.Results;
using RendezVoulns.Application.Models.Entities;

namespace RendezVoulns.Application.Services.Interfaces;

public interface IMembershipService
{
    Task<Result> JoinAsync(Guid userId, Guid groupId, CancellationToken token = default);
    Task<Result<Membership>> UpdateAsync(Membership membership, CancellationToken token = default);
    Task<Result> SoftDeleteAsync(Guid userId, Guid groupId, CancellationToken token = default);
    Task<Result<IEnumerable<Membership>>> GetUserMembershipsAsync(Guid userId, CancellationToken token = default);    
    Task<Result<Membership?>> GetByIdAsync(Guid userId, Guid groupId, CancellationToken token = default);
}