using RendezVoulns.Application.Models.Entities;

namespace RendezVoulns.Application.Repositories.Interfaces;

public interface IGroupRepository
{
    Task<bool> CreateAsync(Group group, CancellationToken token = default);
    Task<Group?> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<IEnumerable<Group>> GetAllAsync(CancellationToken token = default);
    Task<bool> UpdateAsync(Group group, CancellationToken token = default);
    Task<bool> SoftDeleteAsync(Guid id, DateTimeOffset deletedOn, CancellationToken token = default);
    Task<bool> ExistsByIdAsync(Guid id);
}