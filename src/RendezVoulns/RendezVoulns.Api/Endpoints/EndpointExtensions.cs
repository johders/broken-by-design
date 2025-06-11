using RendezVoulns.Api.Endpoints.AppEvents;

namespace RendezVoulns.Api.Endpoints;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapAppEventEndpoints();
        return app;
    }
}