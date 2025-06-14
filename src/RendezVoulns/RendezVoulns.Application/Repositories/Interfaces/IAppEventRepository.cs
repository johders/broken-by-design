using RendezVoulns.Application.Models.Entities;

namespace RendezVoulns.Application.Repositories.Interfaces;

public interface IAppEventRepository
{
    Task<bool> CreateAsync(AppEvent appEvent);
    Task<AppEvent?> GetByIdAsync(Guid id);
    Task<AppEvent?> GetBySlugAsync(string slug);
    Task<IEnumerable<AppEvent>> GetAllAsync();
    Task<bool> UpdateAsync(AppEvent appEvent);
    Task<bool> DeleteByIdAsync(Guid id);
}
