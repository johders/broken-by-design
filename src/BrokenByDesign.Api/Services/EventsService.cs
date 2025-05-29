using BrokenByDesign.Api.Domain;
using BrokenByDesign.Api.Persistence.Repositories;

namespace BrokenByDesign.Api.Services;

public class EventsService(EventsRepository eventsRepository)
{
    private readonly EventsRepository _eventsRepository = eventsRepository;
    public async Task CreateAsync(Event @event)
    {
        await _eventsRepository.CreateAsync(@event);
    }

    public async Task<Event?> GetByIdAsync(Guid eventId)
    {
        return await _eventsRepository.GetByIdAsync(eventId);
    }
}