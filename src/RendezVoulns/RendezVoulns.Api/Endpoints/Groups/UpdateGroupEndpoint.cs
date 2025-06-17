using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Repositories.Interfaces;
using RendezVoulns.Contracts.V1.Group.Requests;

namespace RendezVoulns.Api.Endpoints.Groups;

public static class UpdateGroupEndpoint
{
    public const string Name = "UpdateGroup";

    public static IEndpointRouteBuilder MapUpdateGroup(this IEndpointRouteBuilder app)
    {
        app.MapPut(ApiEndpoints.Groups.Update, async (
            Guid id, UpdateGroupRequest request,
            IGroupRepository repository, CancellationToken token) =>
                {
                    var group = await repository.GetByIdAsync(id, token);

                    if (group is null)
                        return Results.NotFound();

                    var updatedGroup = request.MapToGroup(group);
                    await repository.UpdateAsync(updatedGroup, token);

                    var response = updatedGroup.MapToResponse();
                    return TypedResults.Ok(response);
                })
                .WithName(Name);
        return app;
    }
}