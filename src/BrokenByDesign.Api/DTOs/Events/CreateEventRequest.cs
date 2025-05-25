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
