namespace RendezVoulns.Application.Entities;

public class AppEvent
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid GroupId { get; init; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Location { get; set; }
    public required DateTimeOffset StartTime { get; set; }
    public required DateTimeOffset EndTime { get; set; }
    public required Guid CreatedByUserId { get; init; }
    public DateTimeOffset CreatedOn { get; init; } = DateTimeOffset.UtcNow;
}