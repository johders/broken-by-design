using BrokenByDesign.Api.Persistence.Database;
using BrokenByDesign.Api.Services;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddScoped<EventsService>();
    builder.Services.AddControllers();
}
var app = builder.Build();
{
    app.MapControllers();
    DbInitializer.Initialize(app.Configuration["Database:ConnectionStrings:DefaultConnection"]!);
}
app.Run();