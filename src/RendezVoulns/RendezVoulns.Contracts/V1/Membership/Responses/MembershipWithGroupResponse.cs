namespace RendezVoulns.Contracts.V1.Membership.Responses;

public class MembershipWithGroupResponse
{
    public required GroupSummaryResponse Group { get; set; }
    public required string Role { get; set; }
    public required DateTimeOffset JoinedOn { get; set; }
}

public class GroupSummaryResponse
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}