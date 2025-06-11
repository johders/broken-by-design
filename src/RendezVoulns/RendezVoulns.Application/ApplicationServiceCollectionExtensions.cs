using Microsoft.Extensions.DependencyInjection;
using RendezVoulns.Application.Repositories;
using RendezVoulns.Application.Repositories.Interfaces;

namespace RendezVoulns.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IAppEventRepository, AppEventRepository>();
        return services;
    }
}