namespace RendezVoulns.Contracts.V1.AppEvent.Responses;

public class AppEventResponse
{
    public Guid Id { get; init; }
    public Guid GroupId { get; init; }
    public required string Title { get; init; }
    public required string Slug { get; init; }
    public required string Description { get; init; }
    public required string Location { get; init; }
    public DateTimeOffset StartTime { get; init; }
    public DateTimeOffset EndTime { get; init; }
    public Guid CreatedByUserId { get; init; }
    public DateTimeOffset CreatedOn { get; init; }
}