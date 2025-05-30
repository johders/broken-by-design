using BrokenByDesign.Api.Domain.Common;

namespace BrokenByDesign.Api.Domain.Entities;

public class User : TrackableEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required string PasswordHash { get; init; }
    public DateTimeOffset CreatedOn { get; init; } = DateTimeOffset.UtcNow;
}