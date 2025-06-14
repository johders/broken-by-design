using RendezVoulns.Application.Models.Common;

namespace RendezVoulns.Application.Models.Entities;

public class User : TrackableEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Username { get; init; }
    public required string Email { get; init; }
    public string? ProfileImageUrl { get; set; }
    public DateTimeOffset CreatedOn { get; init; } = DateTimeOffset.UtcNow;  
}