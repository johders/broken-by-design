namespace BrokenByDesign.Api.DTOs.Events;

using BrokenByDesign.Api.Domain.Entities;

public record CreateEventRequest(
        Guid GroupId,
        string Title,
        string Description,
        string Location,
        DateTimeOffset StartTime,
        DateTimeOffset EndTime,
        Guid CreatedByUserId)
{
    public Event ToDomain()
    {
        return new Event
        {
            GroupId = GroupId,
            Title = Title,
            Description = Description,
            Location = Location,
            StartTime = StartTime,
            EndTime = EndTime,
            CreatedByUserId = CreatedByUserId
        };
    }
}
