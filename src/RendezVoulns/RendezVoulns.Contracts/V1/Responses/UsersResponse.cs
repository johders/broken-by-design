namespace RendezVoulns.Contracts.V1.Responses;

public class UsersResponse
{
    public IEnumerable<UserResponse> Items { get; init; } = [];
}