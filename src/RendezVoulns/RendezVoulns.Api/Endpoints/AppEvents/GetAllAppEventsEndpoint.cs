using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Repositories.Interfaces;

namespace RendezVoulns.Api.Endpoints.AppEvents;

public static class GetAllAppEventEndpoint
{
    public const string Name = "GetAppEvents";

    public static IEndpointRouteBuilder MapGetAllAppEvents(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.AppEvents.GetAll, async (
            IAppEventRepository repository, CancellationToken token) =>
                {
                    var appEvents = await repository.GetAllAsync(token);
                    var response = appEvents.MapToResponse();

                    return TypedResults.Ok(response);
                })
                .WithName(Name);
        return app;
    }
}