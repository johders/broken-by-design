using Microsoft.Extensions.Logging;
using RendezVoulns.Application.Common.Errors;
using RendezVoulns.Application.Common.Exceptions;
using RendezVoulns.Application.Common.Results;
using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Application.Repositories.Interfaces;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Application.Services;

public class AppEventService(IAppEventRepository appEventRepository, ILogger<AppEventService> logger) : IAppEventService
{
    private readonly IAppEventRepository _appEventRepository = appEventRepository;

    public async Task<Result> CreateAsync(AppEvent appEvent, CancellationToken token = default)
    {
        try
        {
            var created = await _appEventRepository.CreateAsync(appEvent, token);

            return created
                ? Result.Success()
                : Result.Failure(Errors.AppEvents.CreateFailed);
        }
        catch (DuplicateException ex)
        {
            return Result.Failure(new Error(ex.Code, ex.Message));
        }
        catch (ForeignKeyViolationException ex)
        {
            return Result.Failure(new Error(ex.Code, ex.Message));
        }
    }

    public async Task<Result<AppEvent?>> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        var appEvent = await _appEventRepository.GetByIdAsync(id, token);
        return appEvent is not null
            ? Result<AppEvent?>.Success(appEvent) 
            : Result<AppEvent?>.Failure(Errors.AppEvents.NotFound);
    }

    public async Task<Result<AppEvent?>> GetBySlugAsync(string slug, CancellationToken token = default)
    {
        var appEvent = await _appEventRepository.GetBySlugAsync(slug, token);

        return appEvent is not null
            ? Result<AppEvent?>.Success(appEvent)
            : Result<AppEvent?>.Failure(Errors.AppEvents.NotFound);
    }

    public async Task<Result<IEnumerable<AppEvent>>> GetAllAsync(CancellationToken token = default)
    {
        var appEvents = await _appEventRepository.GetAllAsync(token);
        return Result<IEnumerable<AppEvent>>.Success(appEvents);
    }

    public async Task<Result> UpdateAsync(AppEvent appEvent, CancellationToken token = default)
    {
        try
        {
            var updated = await _appEventRepository.UpdateAsync(appEvent, token);

            return updated
                ? Result.Success()
                : Result.Failure(Errors.AppEvents.UpdateFailed);
        }
        catch (DuplicateException ex)
        {
            return Result.Failure(new Error(ex.Code, ex.Message));
        }
        catch (ForeignKeyViolationException ex)
        {
            return Result.Failure(new Error(ex.Code, ex.Message));
        }
    }
    public async Task<Result> SoftDeleteAsync(Guid id, DateTimeOffset deletedOn, CancellationToken token = default)
    {
        var deleted = await _appEventRepository.SoftDeleteAsync(id, deletedOn, token);

        return deleted
            ? Result.Success()
            : Result.Failure(Errors.AppEvents.DeleteFailed);
    }

    public Task<Result> ExistsByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}