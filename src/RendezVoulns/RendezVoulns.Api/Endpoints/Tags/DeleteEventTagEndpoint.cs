using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Api.Endpoints.Tags;

public static class DeleteEventTagEndpoint
{
    private const string Name = "DeleteEventTag";

    public static IEndpointRouteBuilder MapDeleteEventTag(this IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiEndpoints.AppEvents.DeleteTag, async (
            Guid id, Guid tagId, ITagService service,
            CancellationToken token) =>
            {
                var result = await service.GetByIdAsync(tagId, token)
                    .AndThen(tag => service.DeleteFromEventAsync(id, tagId, token));

                return result.Match(
                    onSuccess: () => TypedResults.Ok(),
                    onFailure: error => error.ToProblem()
                );
            })
            .WithName(Name);
        return app;
    }
}