using RendezVoulns.Application.Models.Common;

namespace RendezVoulns.Application.Models.Entities;

public class Tag : TrackableEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; init; }
    public required string ColorHex { get; init; } = "#cccccc";
    public DateTimeOffset CreatedOn { get; init; } = DateTimeOffset.UtcNow;
}