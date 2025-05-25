namespace BrokenByDesign.Api.Domain;

public class User
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required string PasswordHash { get; init; }
    public DateTime CreatedOn { get; init; } = DateTime.UtcNow;
}