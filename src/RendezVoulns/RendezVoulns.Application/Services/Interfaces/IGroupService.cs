using RendezVoulns.Application.Common.Results;
using RendezVoulns.Application.Models.Entities;

namespace RendezVoulns.Application.Services.Interfaces;

public interface IGroupService
{
    Task<Result> CreateAsync(Group group, CancellationToken token = default);
    Task<Result<Group?>> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<Result<IEnumerable<Group>>> GetAllAsync(CancellationToken token = default);
    Task<Result<Group>> UpdateAsync(Group group, CancellationToken token = default);
    Task<Result> SoftDeleteAsync(Guid id, DateTimeOffset deletedOn, CancellationToken token = default);
}