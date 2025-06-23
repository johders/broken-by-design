using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Application.Persistence.Database;
using RendezVoulns.Application.Repositories.Interfaces;

namespace RendezVoulns.Application.Repositories;

public class MembershipRepository(IDbConnectionFactory dbConnectionFactory) : IMembershipRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public Task<bool> CreateAsync(Membership membership, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Membership?> GetByIdAsync(Guid id, Guid userId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Membership>> GetUserMembershipsAsync(Guid userId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SoftDeleteAsync(Guid userId, Guid groupId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}
