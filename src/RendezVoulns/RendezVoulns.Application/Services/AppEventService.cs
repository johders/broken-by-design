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
        var titleExists = await _appEventRepository.TitleExistsInGroupAsync(appEvent.Title, appEvent.GroupId, token:token);

        if (titleExists)
        {
            return Result.Failure(Errors.AppEvents.DuplicateTitleInGroupError);
        }

        try
        {
            var created = await _appEventRepository.CreateAsync(appEvent, token);

            return created
                ? Result.Success()
                : Result.Failure(Errors.AppEvents.CreateFailedError);
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

    public async Task<Result<AppEvent>> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        var appEvent = await _appEventRepository.GetByIdAsync(id, token);
        return appEvent is not null
            ? Result<AppEvent>.Success(appEvent) 
            : Result<AppEvent>.Failure(Errors.AppEvents.NotFoundError);
    }

    public async Task<Result<AppEvent>> GetBySlugAsync(string slug, CancellationToken token = default)
    {
        var appEvent = await _appEventRepository.GetBySlugAsync(slug, token);

        return appEvent is not null
            ? Result<AppEvent>.Success(appEvent)
            : Result<AppEvent>.Failure(Errors.AppEvents.NotFoundError);
    }

    public async Task<Result<IEnumerable<AppEvent>>> GetAllAsync(CancellationToken token = default)
    {
        var appEvents = await _appEventRepository.GetAllAsync(token);
        return Result<IEnumerable<AppEvent>>.Success(appEvents);
    }

    public async Task<Result<AppEvent>> UpdateAsync(AppEvent appEvent, CancellationToken token = default)
    {
        var eventExists = await _appEventRepository.ExistsByIdAsync(appEvent.Id, token);

        if (!eventExists)
            return Result<AppEvent>.Failure(Errors.AppEvents.NotFoundError);

        var titleExists = await _appEventRepository.TitleExistsInGroupAsync(appEvent.Title, appEvent.GroupId, appEvent.Id, token);

        if (titleExists)
            return Result<AppEvent>.Failure(Errors.AppEvents.DuplicateTitleInGroupError);

        try
        {
            var updated = await _appEventRepository.UpdateAsync(appEvent, token);

            return updated
                ? Result<AppEvent>.Success(appEvent)
                : Result<AppEvent>.Failure(Errors.AppEvents.UpdateFailedError);
        }
        catch (DuplicateException ex)
        {
            return Result<AppEvent>.Failure(new Error(ex.Code, ex.Message));
        }
        catch (ForeignKeyViolationException ex)
        {
            return Result<AppEvent>.Failure(new Error(ex.Code, ex.Message));
        }
    }

    public async Task<Result> SoftDeleteAsync(Guid id, DateTimeOffset deletedOn, CancellationToken token = default)
    {
        var eventExists = await _appEventRepository.ExistsByIdAsync(id, token);

        if (!eventExists)
            return Result.Failure(Errors.AppEvents.NotFoundError);

        var deleted = await _appEventRepository.SoftDeleteAsync(id, deletedOn, token);

        return deleted
            ? Result.Success()
            : Result.Failure(Errors.AppEvents.DeleteFailedError);
    }
}