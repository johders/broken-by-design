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
                    var getResult = await service.GetByIdAsync(id, token);

                    if (getResult.IsFailure || getResult.Value is null)
                    {
                        return getResult.Error!.ToProblem(
                            title: "Event not found",
                            statusCode: StatusCodes.Status404NotFound
                        );
                    }

                    var appEvent = getResult.Value!;

                    var updatedEvent = request.MapToAppEvent(appEvent);

                    var updateResult = await service.UpdateAsync(updatedEvent, token);

                    if (updateResult.IsFailure)
                    {
                        var error = updateResult.Error!;
                        return error.Code switch
                        {
                            Errors.AppEvents.UpdateFailedErrorCode =>
                                error.ToProblem(
                                    title: "Update Failed",
                                    statusCode: StatusCodes.Status500InternalServerError),
                            Errors.AppEvents.DuplicateTitleErrorCode or Errors.AppEvents.DuplicateSlugErrorCode or Errors.AppEvents.DuplicateErrorCode =>
                                error.ToProblem(
                                    title: "Conflict",
                                    statusCode: StatusCodes.Status409Conflict),
                            Errors.AppEvents.InvalidGroupReferenceErrorCode or Errors.AppEvents.InvalidUserReferenceErrorCode =>
                                error.ToProblem(
                                    title: "Bad Request",
                                    statusCode: StatusCodes.Status400BadRequest),
                            _ =>
                                error.ToProblem(
                                    title: "Unexpected Error",
                                    statusCode: StatusCodes.Status500InternalServerError)
                        };
                    }

                    var response = updatedEvent.MapToResponse();
                    return TypedResults.Ok(response);
                })
                .WithName(Name);
        return app;
    }
}