using RendezVoulns.Application.Models.Common;
using RendezVoulns.Application.Models.Enums;

namespace RendezVoulns.Application.Models.Entities;

public class Membership : TrackableEntity
{
    public required Guid UserId { get; init; }
    public required Guid GroupId { get; init; }
    public required GroupRole Role { get; init; } = GroupRole.Member;
    public required DateTimeOffset JoinedOn { get; init; } = DateTimeOffset.UtcNow;
}