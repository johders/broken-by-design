using BrokenByDesign.Api.Services;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddScoped<EventsService>();
    builder.Services.AddControllers();
}
var app = builder.Build();
{
    app.MapControllers();
}
app.Run();