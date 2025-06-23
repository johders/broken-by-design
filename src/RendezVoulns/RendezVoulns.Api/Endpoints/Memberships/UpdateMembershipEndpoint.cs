using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Services.Interfaces;
using RendezVoulns.Contracts.V1.Membership.Requests;

namespace RendezVoulns.Api.Endpoints.Memberships;

public static class UpdateMembershipEndpoint
{
    private const string Name = "UpdateMembership";

    public static IEndpointRouteBuilder MapUpdateMembership(this IEndpointRouteBuilder app)
    {
        app.MapPut(ApiEndpoints.Groups.UpdateMembership, async (
            Guid id, UpdateMembershipRequest request,
            IMembershipService service, CancellationToken token) =>
                {
                    var result = await service.GetByIdAsync(request.UserId, id, token)
                        .AndThen(membership =>
                        {
                            var membershipToUpdate = request.MapToMemberShip(id);
                            return service.UpdateAsync(membershipToUpdate, token);
                        });

                    return result.Match(
                        onSuccess: updatedMembership => TypedResults.Ok(updatedMembership.MapToResponse()),
                        onFailure: error => error.ToProblem());      
                })
                .WithName(Name);
        return app;
    }
}