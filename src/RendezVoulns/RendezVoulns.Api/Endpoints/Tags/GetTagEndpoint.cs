using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Api.Endpoints.Tags;

public static class GetTagEndpoint
{
    public const string Name = "GetTag";

    public static IEndpointRouteBuilder MapGetTag(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Tags.Get, async (
            Guid id, ITagService service, CancellationToken token) =>
                {
                    var result = await service.GetByIdAsync(id, token);

                    return result.Match(
                        onSuccess: tag => TypedResults.Ok(tag!.MapToResponse()),
                        onFailure: error => error.ToProblem());
                })
                .WithName(Name);
        return app;
    }
}