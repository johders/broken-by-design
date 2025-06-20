using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Api.Endpoints.Tags;

public static class DeleteTagEndpoint
{
    private const string Name = "DeleteTag";

    public static IEndpointRouteBuilder MapDeleteTag(this IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiEndpoints.Tags.Delete, async (
            Guid id, ITagService service,
            CancellationToken token) =>
            {
                var result = await service.GetByIdAsync(id, token)
                    .AndThen(tag => service.SoftDeleteAsync(tag!.Id, DateTimeOffset.UtcNow, token));

                return result.Match(
                    onSuccess: () => TypedResults.Ok(),
                    onFailure: error => error.ToProblem()
                );
            })
            .WithName(Name);
        return app;
    }
}