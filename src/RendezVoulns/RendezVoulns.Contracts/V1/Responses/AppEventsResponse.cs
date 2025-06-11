namespace RendezVoulns.Contracts.V1.Responses;

public class AppEventsResponse
{
    public IEnumerable<AppEventResponse> Items { get; init; } = [];
}