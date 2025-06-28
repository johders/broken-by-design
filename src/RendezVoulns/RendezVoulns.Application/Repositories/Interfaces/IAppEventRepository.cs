using RendezVoulns.Application.Models.Entities;

namespace RendezVoulns.Application.Repositories.Interfaces;

public interface IAppEventRepository
{
    Task<bool> CreateAsync(AppEvent appEvent, CancellationToken token = default);
    Task<AppEvent?> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<AppEvent?> GetBySlugAsync(string slug, CancellationToken token = default);
    Task<IEnumerable<AppEvent>> GetAllAsync(CancellationToken token = default);
    Task<bool> UpdateAsync(AppEvent appEvent, CancellationToken token = default);
    Task<bool> SoftDeleteAsync(Guid id, DateTimeOffset deletedOn, CancellationToken token = default);
    Task<bool> ExistsByIdAsync(Guid id, CancellationToken token = default);
    Task<bool> TitleExistsInGroupAsync(string title, Guid groupId, Guid? excludeId = null, CancellationToken token = default);
}
