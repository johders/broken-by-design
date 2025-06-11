namespace RendezVoulns.Contracts.V1.Requests;

public class UpdateAppEventRequest
{
    public Guid? GroupId { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    public string? Location { get; init; }
    public DateTimeOffset? StartTime { get; init; }
    public DateTimeOffset? EndTime { get; init; }
}