using Microsoft.AspNetCore.Http.HttpResults;
using RendezVoulns.Application.Common.Errors;
using RendezVoulns.Application.Models.Entities;

namespace RendezVoulns.Api.Mapping;

public static partial class ErrorMapping
{
    private static ProblemHttpResult MapAppEventErrors(Error error)
    {
        var (statusCode, title) = error.Code switch
        {
            Errors.AppEvents.NotFoundErrorCode => (StatusCodes.Status404NotFound, NotFound),

            Errors.AppEvents.DuplicateSlugErrorCode or
            Errors.AppEvents.DuplicateErrorCode => (StatusCodes.Status409Conflict, Conflict),

            Errors.AppEvents.InvalidGroupErrorCode or
            Errors.AppEvents.InvalidUserErrorCode or
            Errors.AppEvents.InvalidReferenceErrorCode => (StatusCodes.Status400BadRequest, BadRequest),

            Errors.AppEvents.CreateFailedErrorCode or
            Errors.AppEvents.UpdateFailedErrorCode or
            Errors.AppEvents.DeleteFailedErrorCode => (StatusCodes.Status409Conflict, Conflict),

            _ => (StatusCodes.Status500InternalServerError, $"{nameof(AppEvent)}: {Unexpected}")
        };

        return CreateProblemResult(statusCode, title, error);
    }
}

