using System.ComponentModel;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.VisualBasic;
using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Common.Errors;

namespace RendezVoulns.Api.ErrorHandling;

public static class ErrorHandlingExtensions
{
    public static IServiceCollection AddGlobalErrorHandling(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
            };
        });

        return services;
    }

    public static WebApplication UseGlobalErrorHandling(this WebApplication app)
    {
        app.UseExceptionHandler("/error");

        app.Map("/error", (HttpContext httpContext) =>
        {
            var exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            Error error;

            if (exception is null)
            {
                error = new Error("Unhandled.Exception", "An unknown error occurred");
                return error.ToProblem();
            }
            else
            {
                var traceId = httpContext.TraceIdentifier;

                error = new Error("Unhandled.Exception", exception.Message)
                {
                    Extensions = new Dictionary<string, object>
                    {
                        { "traceId", traceId }
                    }
                };
            }

            var env = httpContext.RequestServices.GetRequiredService<IHostEnvironment>();

            if (!env.IsDevelopment())
            {
                error = error with { Message = "An unexpected error occurred" };
            }

            return error.ToProblem();
        });

        return app;
    }
}