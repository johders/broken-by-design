using RendezVoulns.Application.Common.Results;
using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Application.Services;

public class AppEventService : IAppEventService
{
    public Task<Result> CreateAsync(AppEvent appEvent, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> ExistsByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<AppEvent>>> GetAllAsync(CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<AppEvent?>> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<AppEvent?>> GetBySlugAsync(string slug, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> SoftDeleteAsync(Guid id, DateTimeOffset deletedOn, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateAsync(AppEvent appEvent, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}