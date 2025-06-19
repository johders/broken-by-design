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

                    if (result.IsFailure)
                    {
                        return result.Error!.ToProblem(
                            title: "Could not retrieve events",
                            statusCode: StatusCodes.Status500InternalServerError);
                    }

                    var response = result.Value!.MapToResponse();

                    return TypedResults.Ok(response);
                })
                .WithName(Name);
        return app;
    }
}