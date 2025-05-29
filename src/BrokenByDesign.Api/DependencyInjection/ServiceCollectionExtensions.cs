using BrokenByDesign.Api.Persistence.Database;
using BrokenByDesign.Api.Persistence.Repositories;
using BrokenByDesign.Api.Services;

namespace BrokenByDesign.Api.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<EventsService>();

        return services;
    }

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

    public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IDbConnectionFactory>(_ => new NpgsqlConnectionFactory(connectionString!));
        services.AddScoped<EventsRepository>();

        return services;
    }
}