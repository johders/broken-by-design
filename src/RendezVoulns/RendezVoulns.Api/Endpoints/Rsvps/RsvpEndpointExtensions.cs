namespace RendezVoulns.Api.Endpoints.Rsvps;

public static class RsvpEndpointExtensions
{
    public static IEndpointRouteBuilder MapRsvpEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapRsvpEvent();
        app.MapDeleteRsvp();
        app.MapGetUserRsvps();
        return app;        
    }
}