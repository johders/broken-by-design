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
        services.AddScoped<IAppEventRepository, AppEventRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRsvpRepository, RsvpRepository>();
        services.AddScoped<IMembershipRepository, MembershipRepository>();

        services.AddScoped<IAppEventService, AppEventService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IGroupService, GroupService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRsvpService, RsvpService>();
        services.AddScoped<IMembershipService, MembershipService>();
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IDbConnectionFactory>(_ => new NpgsqlConnectionFactory(connectionString));
        return services;
    }
}