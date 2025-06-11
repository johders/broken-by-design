namespace RendezVoulns.Api.Endpoints.AppEvents;

public static class AppEventEndpointExtensions
{
    public static IEndpointRouteBuilder MapAppEventEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCreateAppEvent();
        app.MapGetAppEvent();
        app.MapUpdateAppEvent();
        return app;        
    }
}
