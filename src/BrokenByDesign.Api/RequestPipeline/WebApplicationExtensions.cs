using BrokenByDesign.Api.Errors;
using BrokenByDesign.Api.Persistence.Database;
using Microsoft.AspNetCore.Diagnostics;

namespace BrokenByDesign.Api.RequestPipeline;

public static class WebApplicationExtensions
{
    public static WebApplication InitializeDatabase(this WebApplication app, string connectionString)
    {
        DbInitializer.Initialize(connectionString!);

        return app;
    }  

    public static WebApplication UseGlobalErrorHandling(this WebApplication app)
    {
        app.UseExceptionHandler("/error");

        app.Map("/error", (HttpContext httpContext) =>
        {
            var exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            if (exception is null)
            {
                return Results.Problem();
            }

            return exception switch
            {
                ServiceException serviceException => Results.Problem(
                    statusCode: serviceException.StatusCode,
                    detail: serviceException.ErrorMessage),
                _ => Results.Problem()
            };
        });

        return app;
    }   
}