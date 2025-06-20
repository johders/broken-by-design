using Microsoft.AspNetCore.Http.HttpResults;
using RendezVoulns.Application.Common.Errors;
using RendezVoulns.Application.Models.Entities;

namespace RendezVoulns.Api.Mapping;

public static partial class ErrorMapping
{
    private static ProblemHttpResult MappTagErrors(Error error)
    {
        var (statusCode, title) = error.Code switch
        {
            Errors.Tags.NotFoundErrorCode => (StatusCodes.Status404NotFound, NotFound),

            Errors.Tags.DuplicateNameErrorCode => (StatusCodes.Status409Conflict, Conflict),

            Errors.Tags.CreateFailedErrorCode or
            Errors.Tags.UpdateFailedErrorCode or
            Errors.Tags.DeleteFailedErrorCode => (StatusCodes.Status409Conflict, Conflict),

            _ => (StatusCodes.Status500InternalServerError, $"{nameof(Group)}: {Unexpected}")
        };

        return CreateProblemResult(statusCode, title, error);
    }
}