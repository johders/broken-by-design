using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Repositories.Interfaces;
using RendezVoulns.Contracts.V1.Requests;

namespace RendezVoulns.Api.Endpoints.AppEvents;

public static class UpdateAppEventEndpoint
{
    public const string Name = "UpdateAppEvent";

    public static IEndpointRouteBuilder MapUpdateAppEvent(this IEndpointRouteBuilder app)
    {
        app.MapPut(ApiEndpoints.AppEvents.Update, async (Guid id, UpdateAppEventRequest request, IAppEventRepository repository) =>
            {
                var appEvent = await repository.GetByIdAsync(id);

                if (appEvent is null)
                    return Results.NotFound();

                var updatedEvent = request.MapToAppEvent(id);
                await repository.UpdateAsync(updatedEvent);

                var response = updatedEvent.MapToResponse();
                return TypedResults.Ok(response);
            })
            .WithName(Name);
        return app;
    }
}