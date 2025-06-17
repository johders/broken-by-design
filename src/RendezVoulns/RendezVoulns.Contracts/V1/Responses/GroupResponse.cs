namespace RendezVoulns.Contracts.V1.Responses;

public class GroupResponse
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required DateTimeOffset CreatedOn { get; init; }
}