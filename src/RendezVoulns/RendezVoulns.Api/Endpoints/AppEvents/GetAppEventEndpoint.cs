using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Repositories.Interfaces;

namespace RendezVoulns.Api.Endpoints.AppEvents;

public static class GetAppEventEndpoint
{
    public const string Name = "GetAppEvent";

    public static IEndpointRouteBuilder MapGetAppEvent(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.AppEvents.Get, async (Guid id, IAppEventRepository repository) =>
            {
                var appEvent = await repository.GetByIdAsync(id);

                if (appEvent is null)
                    return Results.NotFound();

                var response = appEvent.MapToResponse();
                return TypedResults.Ok(response);
            })
            .WithName(Name);
        return app;
    }
}