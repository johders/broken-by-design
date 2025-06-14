using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Repositories.Interfaces;

namespace RendezVoulns.Api.Endpoints.AppEvents;

public static class GetAppEventEndpoint
{
    public const string Name = "GetAppEvent";

    public static IEndpointRouteBuilder MapGetAppEvent(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.AppEvents.Get, async (string idOrSlug, IAppEventRepository repository) =>
            {
                var appEvent = Guid.TryParse(idOrSlug, out var id)
                    ? await repository.GetByIdAsync(id)
                    : await repository.GetBySlugAsync(idOrSlug);

                if (appEvent is null)
                    return Results.NotFound();

                var response = appEvent.MapToResponse();
                return TypedResults.Ok(response);
            })
            .WithName(Name);
        return app;
    }
}