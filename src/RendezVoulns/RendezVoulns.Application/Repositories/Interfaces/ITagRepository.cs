using RendezVoulns.Application.Models.Entities;

namespace RendezVoulns.Application.Repositories.Interfaces;

public interface ITagRepository
{
    Task<bool> CreateAsync(Tag tag, CancellationToken token = default);
    Task<Tag?> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<IEnumerable<Tag>> GetAllAsync(CancellationToken token = default);
    Task<bool> UpdateAsync(Tag appEvent, CancellationToken token = default);
    Task<bool> SoftDeleteAsync(Guid id, DateTimeOffset deletedOn, CancellationToken token = default);
}