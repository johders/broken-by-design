namespace BrokenByDesign.Api.DTOs.Events;

using BrokenByDesign.Api.Domain.Entities;

public record EventResponse(
    Guid Id,
    Guid GroupId,
    string Title,
    string Description,
    string Location,
    DateTimeOffset StartTime,
    DateTimeOffset EndTime,
    Guid CreatedByUserId,
    DateTimeOffset CreatedOn)
{
    public static EventResponse FromDomain(Event @event)
    {
        return new EventResponse(
            @event.GroupId,
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