namespace RendezVoulns.Api.Endpoints.Memberships;

public static class MembershipEndpointExtensions
{
    public static IEndpointRouteBuilder MapMembershipEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapJoinGroup();
        app.MapUpdateMembership();
        //app.MapDeleteMembership();
        //app.MapGetUserMembership();
        return app;        
    }
}