using RendezVoulns.Application.Repositories.Interfaces;

namespace RendezVoulns.Api.Endpoints.AppEvents;

public static class DeleteAppEventEndpoint
{
    public const string Name = "DeleteAppEvent";

    public static IEndpointRouteBuilder MapDeleteAppEvent(this IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiEndpoints.AppEvents.Delete, async (
            Guid id, IAppEventRepository repository,
            CancellationToken token) =>
            {
                var appEvent = await repository.GetByIdAsync(id, token);

                if (appEvent is null)
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