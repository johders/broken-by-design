using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Api.Endpoints.Tags;

public static class GetAllTagsEndpoint
{
    private const string Name = "GetTags";

    public static IEndpointRouteBuilder MapGetAllTags(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Tags.GetAll, async (
            ITagService service, CancellationToken token) =>
                {
                    var result = await service.GetAllAsync(token);

                    return result.Match(
                        onSuccess: tags => TypedResults.Ok(tags.MapToResponse()),
                        onFailure: error => error.ToProblem());
                })
                .WithName(Name);
        return app;
    }
}