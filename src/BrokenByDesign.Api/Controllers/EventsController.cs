using System.Net.Http.Headers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class EventsController(EventsService eventsService) : ControllerBase
{
    private readonly EventsService _eventsService = eventsService;

    [HttpPost]
    public IActionResult Create(CreateEventRequest request)
    {
        var @event = request.ToDomain();

        _eventsService.Create(@event);

        return CreatedAtAction(
            actionName: nameof(Get),
            routeValues: new { EventId = @event.Id },
            value: EventResponse.FromDomain(@event));
    }

    [HttpGet("{eventId:guid}")]
    public IActionResult Get(Guid eventId)
    {
        var @event = _eventsService.Get(eventId);
        return @event is null
        ? Problem(statusCode: StatusCodes.Status404NotFound, detail: $"Event not found (eventId: {eventId})")
        : Ok(EventResponse.FromDomain(@event));
    }

    public record CreateEventRequest(
        string Title,
        string Description,
        string Location,
        DateTime StartTime,
        DateTime EndTime,
        Guid CreatedByUserId,
        DateTime CreatedOn)
    {
        public Event ToDomain()
        {
            return new Event
            {
                Title = Title,
                Description = Description,
                Location = Location,
                StartTime = StartTime,
                EndTime = EndTime,
                CreatedByUserId = CreatedByUserId,
                CreatedOn = CreatedOn
            };
        }
    }

    public record EventResponse(
        Guid Id,
        string Title,
        string Description,
        string Location,
        DateTime StartTime,
        DateTime EndTime,
        Guid CreatedByUserId,
        DateTime CreatedOn)
    {
        public static EventResponse FromDomain(Event @event)
        {
            return new EventResponse(
                @event.Id,
                @event.Title,
                @event.Description,
                @event.Location,
                @event.StartTime,
                @event.EndTime,
                @event.CreatedByUserId,
                @event.CreatedOn
            );
        }
    } 
}