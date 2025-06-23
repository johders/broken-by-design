namespace RendezVoulns.Api.Endpoints.Memberships;

public static class MembershipEndpointExtensions
{
    public static IEndpointRouteBuilder MapMembershipEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCreateMembership();
        // app.MapUpdateGroupMembership();
        // app.MapDeleteMembership();
        // app.MapGetUserMembership();
        return app;        
    }
}