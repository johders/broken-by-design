namespace RendezVoulns.Application.Models.Entities;

public class Tag
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; init; }
    public required string ColorHex { get; init; } = "#cccccc";
}