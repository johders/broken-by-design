using Microsoft.AspNetCore.Http.HttpResults;
using RendezVoulns.Application.Common.Errors;
using RendezVoulns.Application.Models.Entities;

namespace RendezVoulns.Api.Mapping;

public static partial class ErrorMapping
{

    private const string NotFound = "Not Found";
    private const string Conflict = "Conflict";
    private const string BadRequest = "Bad Request";
    private const string Unexpected = "Unexpected Error";
    public static IResult ToProblem(this Error error)
    {
        return error.Domain switch
        {
            nameof(AppEvent) => MapAppEventErrors(error),
            // nameof(Group) => MappGroupErrors(error),
            // nameof(Tag) => MappTagErrors(error),
            // nameof(User) => MappUserErrors(error),
            _ => CreateProblemResult(StatusCodes.Status500InternalServerError, Unexpected, error),
        };
    }

    private static ProblemHttpResult CreateProblemResult(int statusCode, string title, Error error) {
        return TypedResults.Problem(
            title: title,
            detail: error.Message,
            statusCode: statusCode,
            extensions: new Dictionary<string, object?> { { "errorCode", error.Code } }
        );
    }
}