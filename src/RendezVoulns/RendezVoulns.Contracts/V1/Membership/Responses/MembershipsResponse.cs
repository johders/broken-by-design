namespace RendezVoulns.Contracts.V1.Membership.Responses;

public class MembershipsResponse
{
    public IEnumerable<MembershipResponse> Items { get; init; } = [];
}