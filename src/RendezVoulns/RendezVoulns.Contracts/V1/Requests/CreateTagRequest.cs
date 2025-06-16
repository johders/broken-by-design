namespace RendezVoulns.Contracts.V1.Requests;

public class CreateTagRequest
{
    public required string Name { get; init; }
    public required string ColorHex { get; init; }
}