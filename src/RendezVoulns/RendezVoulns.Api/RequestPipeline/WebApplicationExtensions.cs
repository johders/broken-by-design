using RendezVoulns.Application.Persistence.Database;

namespace RendezVoulns.Api.RequestPipeline;

public static class WebApplicationExtensions
{
    public static WebApplication InitializeDatabase(this WebApplication app, string connectionString)
    {
        DbInitializer.Initialize(connectionString);
        return app;
    }
}