namespace RendezVoulns.Contracts.V1.User.Requests;

public class UpdateUserRequest
{
    public required string Username { get; init; }
    public required string Email { get; init; }
    public string? ProfileImageUrl { get; set; }
}