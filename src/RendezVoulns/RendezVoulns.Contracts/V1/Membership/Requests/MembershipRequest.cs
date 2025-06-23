namespace RendezVoulns.Contracts.V1.Membership.Requests;

public class UpdateMembershipRequest
{
    public required Guid UserId { get; init; }
    public required string Status { get; init; }
}