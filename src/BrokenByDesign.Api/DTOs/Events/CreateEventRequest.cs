namespace BrokenByDesign.Api.DTOs.Events;

using BrokenByDesign.Api.Domain;

public record CreateEventRequest(
        string Title,
        string Description,
        string Location,
        DateTime StartTime,
        DateTime EndTime,
        Guid CreatedByUserId)
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
            CreatedByUserId = CreatedByUserId
        };
    }
}
