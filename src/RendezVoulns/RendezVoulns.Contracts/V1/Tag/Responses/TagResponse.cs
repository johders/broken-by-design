namespace RendezVoulns.Contracts.V1.Tag.Responses;

public class TagResponse
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string ColorHex { get; init; }
    public required DateTimeOffset CreatedOn { get; init; }
}