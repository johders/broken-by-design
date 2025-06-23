using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Services.Interfaces;
using RendezVoulns.Contracts.V1.Membership.Requests;

namespace RendezVoulns.Api.Endpoints.Memberships;

public static class CreateMembershipEndpoint
{
    private const string Name = "CreateMembership";

    public static IEndpointRouteBuilder MapCreateMembership(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.Groups.Join, async (
            Guid id, MembershipRequest request, IMembershipService service,
            CancellationToken token) =>
                {
                    var userId = Guid.Parse("00000000-0000-0000-0000-000000000003");
                    var membership = id.MapToMemberShip(userId, request.Status);

                    var result = await service.CreateAsync(membership, token);

                    return result.Match(
                        onSuccess: () => TypedResults.Ok(),
                        onFailure: error => error.ToProblem());
                })
                .WithName(Name);
        return app;
    }
}