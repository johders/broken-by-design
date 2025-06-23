using RendezVoulns.Application.Common.Results;
using RendezVoulns.Application.Models.Entities;

namespace RendezVoulns.Application.Services.Interfaces;

public interface IMembershipService
{
    Task<Result> CreateAsync(Membership membership, CancellationToken token = default);
    Task<Result> SoftDeleteAsync(Guid userId, Guid groupId, CancellationToken token = default);
    Task<Result<IEnumerable<Rsvp>>> GetUserMembershipsAsync(Guid userId, CancellationToken token = default);    
    Task<Result<Rsvp?>> GetByIdAsync(Guid id, Guid userId, CancellationToken token = default);
}