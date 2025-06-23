namespace RendezVoulns.Application.ReadModels;

public class MembershipWithGroup
{
    public required Guid UserId { get; init; }
    public required Guid GroupId { get; init; }
    public required string Role { get; init; }
    public required DateTimeOffset JoinedOn { get; init; }
    public GroupSummary Group { get; set; } = default!;
}

public class GroupSummary
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
}