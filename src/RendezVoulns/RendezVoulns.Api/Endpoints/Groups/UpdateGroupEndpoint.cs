using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Services.Interfaces;
using RendezVoulns.Contracts.V1.Group.Requests;

namespace RendezVoulns.Api.Endpoints.Groups;

public static class UpdateGroupEndpoint
{
    private const string Name = "UpdateGroup";

    public static IEndpointRouteBuilder MapUpdateGroup(this IEndpointRouteBuilder app)
    {
        app.MapPut(ApiEndpoints.Groups.Update, async (
            Guid id, UpdateGroupRequest request,
            IGroupService service, CancellationToken token) =>
                {
                    var result = await service.GetByIdAsync(id, token)
                        .AndThen(group =>
                        {
                            var groupToUpdate = request.MapToGroup(group!);
                            return service.UpdateAsync(groupToUpdate, token);
                        });

                    return result.Match(
                        onSuccess: updatedGroup => TypedResults.Ok(updatedGroup.MapToResponse()),
                        onFailure: error => error.ToProblem());      
                })
                .WithName(Name);
        return app;
    }
}