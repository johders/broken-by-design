using RendezVoulns.Application.Common.Errors;
using RendezVoulns.Application.Common.Results;

namespace RendezVoulns.Api.Endpoints;

public static class ResultExtensions
{
    public static IResult ToProblemOld(this Error error, string title, int statusCode)
        => TypedResults.Problem(title: title, detail: error.Message, statusCode: statusCode);

    public static async Task<Result> AndThen<TIn>(this Task<Result<TIn>> resultTask, Func<TIn, Task<Result>> next)
    {
        var initialResult = await resultTask;

        if (initialResult.IsFailure)
            return Result.Failure(initialResult.Error!);

        return await next(initialResult.Value!);
    }

    public static async Task<Result<TOut>> AndThen<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<TIn, Task<Result<TOut>>> next)
    {
        var initialResult = await resultTask;

        if (initialResult.IsFailure)
            return Result<TOut>.Failure(initialResult.Error!);

        return await next(initialResult.Value!);
    }
}