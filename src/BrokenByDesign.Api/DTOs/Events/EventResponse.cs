namespace BrokenByDesign.Api.DTOs.Events;

using BrokenByDesign.Api.Domain;

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