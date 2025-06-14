using RendezVoulns.Application.Models.Common;
using RendezVoulns.Application.Models.Enums;

namespace RendezVoulns.Application.Models.Entities;

public class Rsvp : TrackableEntity
{
    public required Guid UserId { get; init; }
    public required Guid EventId { get; init; }
    public required RsvpStatus Status { get; init; }
    public DateTimeOffset RespondedOn { get; init; } = DateTimeOffset.UtcNow;
}