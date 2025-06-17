namespace RendezVoulns.Contracts.V1.AppEvent.Requests;

public class UpdateAppEventRequest
{
    public required Guid GroupId { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required string Location { get; init; }
    public required DateTimeOffset StartTime { get; init; }
    public required DateTimeOffset EndTime { get; init; }
}