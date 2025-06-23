namespace RendezVoulns.Contracts.V1.Membership.Responses;

public class MembershipsWithGroupResponse
{
    public IEnumerable<MembershipWithGroupResponse> Items { get; init; } = [];
}