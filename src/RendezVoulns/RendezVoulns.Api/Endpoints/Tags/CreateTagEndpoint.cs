using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Repositories.Interfaces;
using RendezVoulns.Contracts.V1.Tag.Requests;

namespace RendezVoulns.Api.Endpoints.Tags;

public static class CreateTagEndpoint
{
    public const string Name = "CreateTag";

    public static IEndpointRouteBuilder MapCreateTag(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.Tags.Create, async (
            CreateTagRequest request, ITagRepository repository, CancellationToken token) =>
                {
                    var tag = request.MapToTag();
                    await repository.CreateAsync(tag, token);

                    var response = tag.MapToResponse();
                    return TypedResults.CreatedAtRoute(response, GetTagEndpoint.Name, new { tag.Id });
                })
                .WithName(Name);
        return app;
    }
}