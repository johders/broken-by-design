namespace BrokenByDesign.Api.Controllers;

using BrokenByDesign.Api.DTOs.Events;
using BrokenByDesign.Api.Services;
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
}