using RendezVoulns.Application.Models.Entities;

namespace RendezVoulns.Application.Repositories.Interfaces;

public interface IMembershipRepository
{
    Task<bool> CreateAsync(Membership membership, CancellationToken token = default);
    Task<bool> SoftDeleteAsync(Guid userId, Guid groupId, CancellationToken token = default);
    Task<IEnumerable<Membership>> GetUserMembershipsAsync(Guid userId, CancellationToken token = default);    
    Task<Membership?> GetByIdAsync(Guid id, Guid userId, CancellationToken token = default);
}