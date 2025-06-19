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

                    if (result.IsFailure || result.Value is null)
                    {
                        return result.Error!.ToProblem(
                            title: "Event not found",
                            statusCode: StatusCodes.Status404NotFound
                        );   
                    }

                    var response = result.Value.MapToResponse();
                    return TypedResults.Ok(response);
                })
                .WithName(Name);
        return app;
    }
}