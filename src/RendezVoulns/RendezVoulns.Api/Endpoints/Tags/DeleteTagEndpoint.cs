using RendezVoulns.Application.Repositories.Interfaces;

namespace RendezVoulns.Api.Endpoints.Tags;

public static class DeleteTagEndpoint
{
    public const string Name = "DeleteTag";

    public static IEndpointRouteBuilder MapDeleteTag(this IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiEndpoints.Tags.Delete, async (
            Guid id, ITagRepository repository,
            CancellationToken token) =>
            {
                var tag = await repository.GetByIdAsync(id, token);

                if (tag is null)
                    return Results.NotFound();

                bool deleted = await repository.SoftDeleteAsync(id, DateTimeOffset.UtcNow, token);

                return deleted
                ? TypedResults.Ok()
                : Results.StatusCode(StatusCodes.Status500InternalServerError);
            })
            .WithName(Name);
        return app;
    }
}