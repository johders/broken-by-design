namespace RendezVoulns.Contracts.V1.AppEvent.Requests;

public interface IAppEventRequest
{
    Guid GroupId { get; }
    string Title { get; }
    string Description { get; }
    string Location { get; }
    DateTimeOffset StartTime { get; }
    DateTimeOffset EndTime { get; }
}