using BrokenByDesign.Api.Domain.Common;

namespace BrokenByDesign.Api.Domain.Entities;

public class Group : TrackableEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; init; }
    public string Description { get; init; } = string.Empty;
    public DateTimeOffset CreatedOn { get; init; } = DateTimeOffset.UtcNow;

}