using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Common.Errors;
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
                    {
                        var error = result.Error!;
                        return error.Code switch
                        {
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

                    var response = appEvent.MapToResponse();
                    return TypedResults.CreatedAtRoute(response, GetAppEventEndpoint.Name, new {idOrSlug = appEvent.Slug});
                })
                .WithName(Name);
        return app;
    }
}