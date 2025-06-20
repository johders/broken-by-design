using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Api.Endpoints.AppEvents;

public static class DeleteAppEventEndpoint
{
    private const string Name = "DeleteAppEvent";

    public static IEndpointRouteBuilder MapDeleteAppEvent(this IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiEndpoints.AppEvents.Delete, async (
            Guid id, IAppEventService service,
            CancellationToken token) =>
            {
                var result = await service.GetByIdAsync(id, token)
                    .AndThen(appEvent => service.SoftDeleteAsync(appEvent!.Id, DateTimeOffset.UtcNow, token));

                return result.Match(
                    onSuccess: () => TypedResults.Ok(),
                    onFailure: error => error.ToProblem());
            })
            .WithName(Name);
        return app;
    }
}