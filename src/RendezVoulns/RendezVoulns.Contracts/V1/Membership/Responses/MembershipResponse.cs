namespace RendezVoulns.Contracts.V1.Membership.Responses;

public class MembershipResponse
{
    public required Guid UserId { get; init; }
    public required Guid GroupId { get; init; }
    public required string Role { get; init; }
    public required DateTimeOffset JoinedOn { get; init; }
}