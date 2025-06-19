using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Api.Endpoints.AppEvents;

public static class DeleteAppEventEndpoint
{
    public const string Name = "DeleteAppEvent";

    public static IEndpointRouteBuilder MapDeleteAppEvent(this IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiEndpoints.AppEvents.Delete, async (
            Guid id, IAppEventService service,
            CancellationToken token) =>
            {
                var getResult = await service.GetByIdAsync(id, token);

                if (getResult.IsFailure)
                {
                    return getResult.Error!.ToProblem(
                        title: "Not Found",
                        statusCode: StatusCodes.Status404NotFound);
                }

                var deleteResult = await service.SoftDeleteAsync(id, DateTimeOffset.UtcNow, token);

                if (deleteResult.IsFailure)
                {
                    return deleteResult.Error!.ToProblem(
                        title: "Delete Failed",
                        statusCode: StatusCodes.Status500InternalServerError
                    );
                }

                return TypedResults.Ok();
            })
            .WithName(Name);
        return app;
    }
}