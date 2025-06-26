using RendezVoulns.Api.Mapping;
using RendezVoulns.Api.Validation;
using RendezVoulns.Application.Services.Interfaces;
using RendezVoulns.Contracts.V1.Group.Requests;

namespace RendezVoulns.Api.Endpoints.Groups;

public static class CreateGroupEndpoint
{
    private const string Name = "CreateGroup";

    public static IEndpointRouteBuilder MapCreateGroup(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.Groups.Create, async (
            CreateGroupRequest request, IGroupService service, CancellationToken token) =>
                {
                    var group = request.MapToGroup();
                    var result = await service.CreateAsync(group, token);

                    return result.Match(
                        onSuccess: () => TypedResults.CreatedAtRoute(group.MapToResponse(), GetGroupEndpoint.Name, new { group.Id }),
                        onFailure: error => error.ToProblem());
                })
                .WithName(Name)
                .WithValidation<CreateGroupRequest>();
        return app;
    }
}