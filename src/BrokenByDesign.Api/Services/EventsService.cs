using BrokenByDesign.Api.Domain;
using BrokenByDesign.Api.Errors;
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

        var result = await _eventsRepository.GetByIdAsync(eventId);

        if (result is null)
        {
            throw new NotFoundException($"Event not found (eventId: {eventId})");
        }

        return result;
    }
}