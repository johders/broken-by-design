using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Common.Errors;
using RendezVoulns.Application.Services.Interfaces;
using RendezVoulns.Contracts.V1.AppEvent.Requests;

namespace RendezVoulns.Api.Endpoints.AppEvents;

public static class UpdateAppEventEndpoint
{
    public const string Name = "UpdateAppEvent";

    public static IEndpointRouteBuilder MapUpdateAppEvent(this IEndpointRouteBuilder app)
    {
        app.MapPut(ApiEndpoints.AppEvents.Update, async (
            Guid id, UpdateAppEventRequest request,
            IAppEventService service, CancellationToken token) =>
                {
                    var result = await service.GetByIdAsync(id, token)
                        .AndThen(appEvent =>
                        {
                            var eventToUpdate = request.MapToAppEvent(appEvent!);
                            return service.UpdateAsync(eventToUpdate, token);
                        });

                    return result.Match(
                        onSuccess: updatedEvent => TypedResults.Ok(updatedEvent.MapToResponse()),
                        onFailure: error => error.ToProblem()
                    );                
                })
                .WithName(Name);
        return app;
    }
}