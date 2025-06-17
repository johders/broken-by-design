using Microsoft.Extensions.DependencyInjection;
using RendezVoulns.Application.Persistence.Database;
using RendezVoulns.Application.Repositories;
using RendezVoulns.Application.Repositories.Interfaces;

namespace RendezVoulns.Application.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IAppEventRepository, AppEventRepository>();
        services.AddSingleton<ITagRepository, TagRepository>();
        services.AddSingleton<IGroupRepository, GroupRepository>();
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IDbConnectionFactory>(_ => new NpgsqlConnectionFactory(connectionString));
        return services;
    }
}