using RendezVoulns.Application.Common.Errors;

namespace RendezVoulns.Api.Endpoints;

public static class ResultExtensions
{
    public static IResult ToProblem(this Error error, string title, int statusCode)
        => TypedResults.Problem(title: title, detail: error.Message, statusCode: statusCode);
}