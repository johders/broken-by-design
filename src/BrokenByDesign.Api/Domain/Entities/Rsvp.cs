using BrokenByDesign.Api.Domain.Common;
using BrokenByDesign.Api.Domain.Enums;

namespace BrokenByDesign.Api.Domain.Entities;

public class Rsvp : TrackableEntity
{
    public required Guid UserId { get; init; }
    public required Guid EventId { get; init; }
    public required RsvpStatus Status { get; init; }
    public DateTimeOffset RespondedOn { get; init; } = DateTimeOffset.UtcNow;
}