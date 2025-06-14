using RendezVoulns.Api.Endpoints;
using RendezVoulns.Api.RequestPipeline;
using RendezVoulns.Application.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
string connectionString = config["Database:ConnectionString"]!;

builder.Services.AddOpenApi();

builder.Services
    .AddApplication()
    .AddDatabase(connectionString);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

{
    app.UseHttpsRedirection();
    app.MapApiEndpoints();
    app.InitializeDatabase(connectionString);
}

app.Run();
