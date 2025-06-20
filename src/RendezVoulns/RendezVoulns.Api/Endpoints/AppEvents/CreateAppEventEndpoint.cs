using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Services.Interfaces;
using RendezVoulns.Contracts.V1.AppEvent.Requests;

namespace RendezVoulns.Api.Endpoints.AppEvents;

public static class CreateAppEventEndpoint
{
    public const string Name = "CreateAppEvent";

    public static IEndpointRouteBuilder MapCreateAppEvent(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.AppEvents.Create, async (
            CreateAppEventRequest request, IAppEventService service,
            CancellationToken token) =>
                {
                    var appEvent = request.MapToAppEvent();
                    var result = await service.CreateAsync(appEvent, token);

                    if (result.IsFailure)
                        return result.Error!.ToProblemDetails();

                    var response = appEvent.MapToResponse();
                    return TypedResults.CreatedAtRoute(response, GetAppEventEndpoint.Name, new {idOrSlug = appEvent.Slug});
                })
                .WithName(Name);
        return app;
    }
}