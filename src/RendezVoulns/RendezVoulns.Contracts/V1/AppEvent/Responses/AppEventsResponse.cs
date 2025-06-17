namespace RendezVoulns.Contracts.V1.AppEvent.Responses;

public class AppEventsResponse
{
    public IEnumerable<AppEventResponse> Items { get; init; } = [];
}