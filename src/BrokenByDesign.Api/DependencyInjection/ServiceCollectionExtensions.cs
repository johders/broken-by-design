using BrokenByDesign.Api.Services;

namespace BrokenByDesign.Api.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<EventsService>();

        return services;
    }
}