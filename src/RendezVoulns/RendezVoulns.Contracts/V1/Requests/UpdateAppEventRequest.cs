namespace RendezVoulns.Contracts.V1.Requests;

public class UpdateAppEventRequest
{
    public Guid GroupId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
    public DateTimeOffset StartTime { get; init; }
    public DateTimeOffset EndTime { get; init; }
}