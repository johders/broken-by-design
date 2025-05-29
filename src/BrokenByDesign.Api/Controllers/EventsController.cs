using BrokenByDesign.Api.DTOs.Events;
using BrokenByDesign.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BrokenByDesign.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EventsController(EventsService eventsService) : ControllerBase
{
    private readonly EventsService _eventsService = eventsService;

    [HttpPost]
    public async Task<IActionResult> Create(CreateEventRequest request)
    {
        var @event = request.ToDomain();

        await _eventsService.CreateAsync(@event);

        return CreatedAtAction(
            actionName: nameof(Get),
            routeValues: new { EventId = @event.Id },
            value: EventResponse.FromDomain(@event));
    }

    [HttpGet("{eventId:guid}")]
    public async Task<IActionResult> Get(Guid eventId)
    {
        var @event = await _eventsService.GetByIdAsync(eventId);
        return @event is null
        ? Problem(statusCode: StatusCodes.Status404NotFound, detail: $"Event not found (eventId: {eventId})")
        : Ok(EventResponse.FromDomain(@event));
    }
}