using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Application.ReadModels;

namespace RendezVoulns.Application.Repositories.Interfaces;

public interface IMembershipRepository
{
    Task<bool> JoinAsync(Guid userId, Guid groupId, CancellationToken token = default);
    Task<bool> UpdateAsync(Membership membership, CancellationToken token = default);
    Task<bool> SoftDeleteAsync(Guid userId, Guid groupId, CancellationToken token = default);
    Task<IEnumerable<MembershipWithGroup>> GetUserMembershipsAsync(Guid userId, CancellationToken token = default);    
    Task<Membership?> GetByIdAsync(Guid userId, Guid groupId, CancellationToken token = default);
}