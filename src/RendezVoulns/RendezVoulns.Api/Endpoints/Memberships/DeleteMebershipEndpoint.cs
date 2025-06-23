using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Api.Endpoints.Memberships;

public static class DeleteMembershipEndpoint
{
    private const string Name = "DeleteMembership";

    public static IEndpointRouteBuilder MapDeleteMembership(this IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiEndpoints.Groups.DeleteMembership, async (
            Guid id, Guid userId, IMembershipService service,
            CancellationToken token) =>
            {
                var result = await service.GetByIdAsync(userId, id, token)
                    .AndThen(rsvp => service.SoftDeleteAsync(userId, id, token));

                return result.Match(
                    onSuccess: () => TypedResults.Ok(),
                    onFailure: error => error.ToProblem()
                );
            })
            .WithName(Name);
        return app;
    }
}