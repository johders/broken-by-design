namespace RendezVoulns.Contracts.V1.Responses;

public class TagsResponse
{
    public IEnumerable<TagResponse> Items { get; init; } = [];
}