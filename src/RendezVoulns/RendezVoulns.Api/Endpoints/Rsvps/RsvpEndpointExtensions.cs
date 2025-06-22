namespace RendezVoulns.Api.Endpoints.Rsvps;

public static class RsvpEndpointExtensions
{
    public static IEndpointRouteBuilder MapRsvpEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapRsvpEvent();
        // app.MapDeleteRsvp();
        // app.MapUpdateRsvp();
        // app.MapGetUserRsvps();
        return app;        
    }
}