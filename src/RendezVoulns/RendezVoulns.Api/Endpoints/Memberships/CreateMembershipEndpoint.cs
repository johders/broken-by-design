using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Api.Endpoints.Memberships;

public static class JoinGroupEndpoint
{
    private const string Name = "JoinGroup";

    public static IEndpointRouteBuilder MapJoinGroup(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.Groups.Join, async (
            Guid id, IMembershipService service,
            CancellationToken token) =>
                {
                    var userId = Guid.Parse("00000000-0000-0000-0000-000000000003");

                    var result = await service.JoinAsync(userId, id, token);

                    return result.Match(
                        onSuccess: () => TypedResults.Ok(),
                        onFailure: error => error.ToProblem());
                })
                .WithName(Name);
        return app;
    }
}