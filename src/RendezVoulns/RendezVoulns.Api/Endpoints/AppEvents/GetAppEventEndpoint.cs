using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Api.Endpoints.AppEvents;

public static class GetAppEventEndpoint
{
    public const string Name = "GetAppEvent";

    public static IEndpointRouteBuilder MapGetAppEvent(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.AppEvents.Get, async (
            string idOrSlug, IAppEventService service,
            CancellationToken token) =>
                {
                    var result = Guid.TryParse(idOrSlug, out var id)
                        ? await service.GetByIdAsync(id, token)
                        : await service.GetBySlugAsync(idOrSlug, token);

                    return result.Match(
                        onSuccess: appEvent => TypedResults.Ok(appEvent!.MapToResponse()),
                        onFailure: error => error.ToProblem()
                    );
                })
                .WithName(Name);
        return app;
    }
}