using RendezVoulns.Api.Endpoints.AppEvents;
using RendezVoulns.Api.Endpoints.Groups;
using RendezVoulns.Api.Endpoints.Rsvps;
using RendezVoulns.Api.Endpoints.Tags;
using RendezVoulns.Api.Endpoints.Users;

namespace RendezVoulns.Api.Endpoints;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapAppEventEndpoints();
        app.MapTagEndpoints();
        app.MapGroupEndpoints();
        app.MapUserEndpoints();
        app.MapRsvpEndpoints();
        return app;
    }
}