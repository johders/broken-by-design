using Microsoft.AspNetCore.Http.HttpResults;
using RendezVoulns.Application.Common.Errors;
using RendezVoulns.Application.Models.Entities;

namespace RendezVoulns.Api.Mapping;

public static partial class ErrorMapping
{
    private static ProblemHttpResult MapUserErrors(Error error)
    {
        var (statusCode, title) = error.Code switch
        {
            Errors.Users.NotFoundErrorCode => (StatusCodes.Status404NotFound, NotFound),

            Errors.Users.DuplicateEmailErrorCode or
            Errors.Users.DuplicateUsernameErrorCode or
            Errors.Users.DuplicateSlugErrorCode or
            Errors.Users.DuplicateErrorCode => (StatusCodes.Status409Conflict, Conflict),

            Errors.Users.CreateFailedErrorCode or
            Errors.Users.UpdateFailedErrorCode or
            Errors.Users.DeleteFailedErrorCode => (StatusCodes.Status409Conflict, Conflict),

            _ => (StatusCodes.Status500InternalServerError, $"{nameof(User)}: {Unexpected}")
        };

        return CreateProblemResult(statusCode, title, error);
    }
}