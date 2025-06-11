using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Repositories.Interfaces;
using RendezVoulns.Contracts.V1.Requests;

namespace RendezVoulns.Api.Endpoints.AppEvents;

public static class CreateAppEventEndpoint
{
    public const string Name = "CreateAppEvent";

    public static IEndpointRouteBuilder MapCreateAppEvent(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.AppEvents.Create, async (CreateAppEventRequest request, IAppEventRepository repository) =>
        {
            var appEvent = request.MapToAppEvent();
            await repository.CreateAsync(appEvent);

            var response = appEvent.MapToResponse();
            return TypedResults.Ok(response);
        })
        .WithName(Name);
        return app;
    }
}