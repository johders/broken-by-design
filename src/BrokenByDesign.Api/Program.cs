using BrokenByDesign.Api.DependencyInjection;
using BrokenByDesign.Api.RequestPipeline;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddGlobalErrorHandling()
        .AddServices()
        .AddPersistence(builder.Configuration)
        .AddControllers();
}

var app = builder.Build();
{
    app.UseGlobalErrorHandling();
    app.MapControllers();
    app.InitializeDatabase();
}

app.Run();