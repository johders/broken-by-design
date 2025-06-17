namespace RendezVoulns.Api.Endpoints.Users;

public static class UserEndpointExtensions{

    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCreateUser();
        // app.MapGetUser();
        // app.MapGetAllUsers();
        // app.MapUpdateUser();
        // app.MapDeleteUser();
        return app;
    }
}