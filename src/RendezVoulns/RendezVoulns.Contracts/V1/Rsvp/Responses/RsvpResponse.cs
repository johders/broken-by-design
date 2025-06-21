namespace RendezVoulns.Contracts.V1.Rsvp.Responses;

public class RsvpResponse
{
    public required Guid UserId { get; init; }
    public required Guid EventId { get; init; }
    public required string Status { get; init; }
    public required DateTimeOffset RespondedOn { get; init; }
}