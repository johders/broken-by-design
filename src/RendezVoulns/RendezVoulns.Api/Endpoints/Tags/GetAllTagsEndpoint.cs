using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Repositories.Interfaces;

namespace RendezVoulns.Api.Endpoints.Tags;

public static class GetAllTagsEndpoint
{
    public const string Name = "GetTags";

    public static IEndpointRouteBuilder MapGetAllTags(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Tags.GetAll, async (
            ITagRepository repository, CancellationToken token) =>
                {
                    var tags = await repository.GetAllAsync(token);

                    var response = tags.MapToResponse();
                    return TypedResults.Ok(response);
                })
                .WithName(Name);
        return app;
    }
}