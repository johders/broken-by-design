using Microsoft.AspNetCore.Components.Web;

public class EventsService
{
    private static readonly List<Event> EventsRepostitory = [];
    public void Create(Event @event)
    {
        EventsRepostitory.Add(@event);
    }
    
    public Event? Get(Guid eventId) {
        return EventsRepostitory.Find(e => e.Id == eventId);
    }
}