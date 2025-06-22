namespace RendezVoulns.Api.Endpoints.Tags;

public static class TagEndpointExtensions{

    public static IEndpointRouteBuilder MapTagEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCreateTag();
        app.MapGetTag();
        app.MapGetAllTags();
        app.MapUpdateTag();
        app.MapDeleteTag();
        app.MapDeleteEventTag();
        app.MapTagEvent();
        return app;
    }
}

