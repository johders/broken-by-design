namespace RendezVoulns.Contracts.V1.Responses;

public class UserResponse
{
    public Guid Id { get; init; }
    public required string Username { get; init; }
    public required string Email { get; init; }
    public string? ProfileImageUrl { get; set; }
    public required DateTimeOffset CreatedOn { get; init; }
}