using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Application.Repositories.Interfaces;

namespace RendezVoulns.Application.Repositories;

public class AppEventRepository : IAppEventRepository
{
    private readonly List<AppEvent> _appEvents = [];
    public Task<bool> CreateAsync(AppEvent appEvent)
    {
        _appEvents.Add(appEvent);
        return Task.FromResult(true);
    }

    public Task<AppEvent?> GetByIdAsync(Guid id)
    {
        var appEvent = _appEvents.SingleOrDefault(e => e.Id == id);
        return Task.FromResult(appEvent);
    }

    public Task<AppEvent?> GetBySlugAsync(string slug)
    {
        var appEvent = _appEvents.SingleOrDefault(e => e.Slug == slug);
        return Task.FromResult(appEvent);
    }

    public Task<IEnumerable<AppEvent>> GetAllAsync()
    {
        return Task.FromResult(_appEvents.AsEnumerable());
    }


    public Task<bool> UpdateAsync(AppEvent appEvent)
    {
        var appEventIndex = _appEvents.FindIndex(e => e.Id == appEvent.Id);

        if (appEventIndex == -1)
        {
            return Task.FromResult(false);
        }

        _appEvents[appEventIndex] = appEvent;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteByIdAsync(Guid id)
    {
        var removedCount = _appEvents.RemoveAll(e => e.Id == id);
        var appEventRemoved = removedCount > 0;

        return Task.FromResult(appEventRemoved);
    }
}