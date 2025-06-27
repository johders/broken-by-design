namespace RendezVoulns.Contracts.V1.Tag.Requests;

public interface ITagRequest
{
    public string Name { get; }
    public string ColorHex { get; }
}