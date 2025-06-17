namespace RendezVoulns.Contracts.V1.Group.Responses;

public class GroupsResponse
{
    public IEnumerable<GroupResponse> Items { get; init; } = [];
}