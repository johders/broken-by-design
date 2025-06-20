using Microsoft.AspNetCore.Http.HttpResults;
using RendezVoulns.Application.Common.Errors;
using RendezVoulns.Application.Models.Entities;

namespace RendezVoulns.Api.Mapping;

public static partial class ErrorMapping
{
    private static ProblemHttpResult MapGroupErrors(Error error)
    {
        var (statusCode, title) = error.Code switch
        {
            Errors.Groups.NotFoundErrorCode => (StatusCodes.Status404NotFound, NotFound),

            Errors.Groups.DuplicateNameErrorCode => (StatusCodes.Status409Conflict, Conflict),

            Errors.Groups.CreateFailedErrorCode or
            Errors.Groups.UpdateFailedErrorCode or
            Errors.Groups.DeleteFailedErrorCode => (StatusCodes.Status409Conflict, Conflict),

            _ => (StatusCodes.Status500InternalServerError, $"{nameof(Group)}: {Unexpected}")
        };

        return CreateProblemResult(statusCode, title, error);
    }
}