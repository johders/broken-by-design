namespace RendezVoulns.Contracts.V1.Group.Requests;

public class CreateGroupRequest
{
    public required string Name { get; init; }
    public required string Description { get; init; }
}