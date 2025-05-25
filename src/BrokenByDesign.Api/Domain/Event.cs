namespace BrokenByDesign.Api.Domain;

public class Event
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required string Location { get; init; }
    public required DateTime StartTime { get; init; }
    public required DateTime EndTime { get; init; }
    public required Guid CreatedByUserId { get; init; }
    public DateTime CreatedOn { get; init; } = DateTime.UtcNow;
}