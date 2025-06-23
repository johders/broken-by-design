using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Api.Endpoints.Memberships;

public static class GetUserMembershipsEndpoint
{
    private const string Name = "GetUserMemberships";

    public static IEndpointRouteBuilder MapGetUserMemberships(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Memberships.GetUserMemberships, async (
            IMembershipService service, CancellationToken token) =>
                {
                    var userId = Guid.Parse("00000000-0000-0000-0000-000000000003");
                    var result = await service.GetUserMembershipsAsync(userId, token);

                    return result.Match(
                        onSuccess: memberships => TypedResults.Ok(memberships.MapToResponse()),
                        onFailure: error => error.ToProblem());
                })
                .WithName(Name);
        return app;
    }
}