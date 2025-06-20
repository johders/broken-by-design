using RendezVoulns.Application.Common.Results;
using RendezVoulns.Application.Models.Entities;

namespace RendezVoulns.Application.Services.Interfaces;

public interface IAppEventService
{
    Task<Result> CreateAsync(AppEvent appEvent, CancellationToken token = default);
    Task<Result<AppEvent?>> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<Result<AppEvent?>> GetBySlugAsync(string slug, CancellationToken token = default);
    Task<Result<IEnumerable<AppEvent>>> GetAllAsync(CancellationToken token = default);
    Task<Result<AppEvent>> UpdateAsync(AppEvent appEvent, CancellationToken token = default);
    Task<Result> SoftDeleteAsync(Guid id, DateTimeOffset deletedOn, CancellationToken token = default);
    Task<Result> ExistsByIdAsync(Guid id);
}