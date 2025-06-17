namespace RendezVoulns.Api.Endpoints.Groups;

public static class GroupEndpointExtensions{

    public static IEndpointRouteBuilder MapGroupEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCreateGroup();
        app.MapGetGroup();
        // app.MapGetAllGroups();
        // app.MapUpdateGroup();
        // app.MapDeleteGroup();
        return app;
    }
}