using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Api.Endpoints.AppEvents;

public static class GetAllAppEventEndpoint
{
    public const string Name = "GetAppEvents";

    public static IEndpointRouteBuilder MapGetAllAppEvents(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.AppEvents.GetAll, async (
            IAppEventService service, CancellationToken token) =>
                {
                    var result = await service.GetAllAsync(token);

                    return result.Match(
                        onSuccess: events => TypedResults.Ok(events.MapToResponse()),
                        onFailure: error => error.ToProblem()
                    );
                })
                .WithName(Name);
        return app;
    }
}