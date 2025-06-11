namespace RendezVoulns.Contracts.V1.Responses;

public class AppEventResponse
{
    public Guid Id { get; init; }
    public Guid GroupId { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    public string? Location { get; init; }
    public DateTimeOffset StartTime { get; init; }
    public DateTimeOffset EndTime { get; init; }
    public Guid CreatedByUserId { get; init; }
    public DateTimeOffset CreatedOn { get; init; }
}