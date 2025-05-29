using BrokenByDesign.Api.Persistence;
using BrokenByDesign.Api.Persistence.Database;

namespace BrokenByDesign.Api.RequestPipeline;

public static class WebApplicationExtensions
{
    public static WebApplication InitializeDatabase(this WebApplication app)
    {
        DbInitializer.Initialize(app.Configuration[DbConstants.DefaultConnectionStringPath]!);
        return app;
    }   
}