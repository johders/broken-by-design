namespace RendezVoulns.Contracts.V1.Responses;

public class GroupsResponse
{
    public IEnumerable<GroupResponse> Items { get; init; } = [];
}