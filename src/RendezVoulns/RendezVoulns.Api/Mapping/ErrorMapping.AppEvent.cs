namespace RendezVoulns.Api.Mapping;

using Microsoft.AspNetCore.Http.HttpResults;
using RendezVoulns.Application.Common.Errors;
using RendezVoulns.Application.Models.Entities;

public static partial class ErrorMapping
{
    private static ProblemHttpResult MapAppEventErrors(Error error)
    {
        var (statusCode, title) = error.Code switch
        {
            Errors.AppEvents.NotFoundErrorCode => (StatusCodes.Status404NotFound, NotFound),

            Errors.AppEvents.DuplicateTitleErrorCode or
            Errors.AppEvents.DuplicateSlugErrorCode or
            Errors.AppEvents.DuplicateErrorCode => (StatusCodes.Status409Conflict, Conflict),

            Errors.AppEvents.InvalidGroupErrorCode or
            Errors.AppEvents.InvalidUserErrorCode => (StatusCodes.Status400BadRequest, BadRequest),

            _ => (StatusCodes.Status500InternalServerError, $"{nameof(AppEvent)}: {Unexpected}" )
        };

        return CreateProblemResult(statusCode, title, error);
    }
}

