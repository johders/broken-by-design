namespace RendezVoulns.Contracts.V1.User.Responses;

public class UsersResponse
{
    public IEnumerable<UserResponse> Items { get; init; } = [];
}