namespace RendezVoulns.Contracts.V1.Tag.Responses;

public class TagsResponse
{
    public IEnumerable<TagResponse> Items { get; init; } = [];
}