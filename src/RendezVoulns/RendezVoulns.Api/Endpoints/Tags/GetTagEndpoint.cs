using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Repositories.Interfaces;

namespace RendezVoulns.Api.Endpoints.Tags;

public static class GetTagEndpoint
{
    public const string Name = "GetTag";

    public static IEndpointRouteBuilder MapGetTag(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Tags.Get, async (
            Guid id, ITagRepository repository, CancellationToken token) =>
                {
                    var tag = await repository.GetByIdAsync(id, token);

                    if (tag is null)
                        return Results.NotFound();

                    var response = tag.MapToResponse();
                    return TypedResults.Ok(response);
                })
                .WithName(Name);
        return app;
    }
}