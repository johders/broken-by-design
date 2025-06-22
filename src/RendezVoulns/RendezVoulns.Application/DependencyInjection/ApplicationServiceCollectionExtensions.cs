using Microsoft.Extensions.DependencyInjection;
using RendezVoulns.Application.Persistence.Database;
using RendezVoulns.Application.Repositories;
using RendezVoulns.Application.Repositories.Interfaces;
using RendezVoulns.Application.Services;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Application.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IAppEventRepository, AppEventRepository>();
        services.AddSingleton<ITagRepository, TagRepository>();
        services.AddSingleton<IGroupRepository, GroupRepository>();
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IRsvpRepository, RsvpRepository>();

        services.AddSingleton<IAppEventService, AppEventService>();
        services.AddSingleton<ITagService, TagService>();
        services.AddSingleton<IGroupService, GroupService>();
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IRsvpService, RsvpService>();
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IDbConnectionFactory>(_ => new NpgsqlConnectionFactory(connectionString));
        return services;
    }
}