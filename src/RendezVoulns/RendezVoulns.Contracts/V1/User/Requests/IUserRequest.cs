namespace RendezVoulns.Contracts.V1.User.Requests;

public interface IUserRequest
{
    public string Username { get; }
    public string Email { get; }
    public string? ProfileImageUrl { get; }
}