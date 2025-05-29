using BrokenByDesign.Api.DependencyInjection;
using BrokenByDesign.Api.Persistence;
using BrokenByDesign.Api.RequestPipeline;

var builder = WebApplication.CreateBuilder(args);

var dbConnectionString = builder.Environment.IsDevelopment()
    ? builder.Configuration.GetConnectionString("DefaultConnection")!
    : builder.Configuration[DbConstants.DefaultConnectionStringPath]!;
    
{
    builder.Services
        .AddGlobalErrorHandling()
        .AddServices()
        .AddPersistence(dbConnectionString)
        .AddControllers();
}

var app = builder.Build();
{
    app.UseGlobalErrorHandling();
    app.MapControllers();
    app.InitializeDatabase(dbConnectionString);
}

app.Run();