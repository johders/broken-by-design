using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Repositories.Interfaces;
using RendezVoulns.Contracts.V1.Requests;

namespace RendezVoulns.Api.Endpoints.Groups;

public static class CreateGroupEndpoint
{
    public const string Name = "CreateGroup";

    public static IEndpointRouteBuilder MapCreateGroup(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.Groups.Create, async (
            CreateGroupRequest request, IGroupRepository repository, CancellationToken token) =>
                {
                    var group = request.MapToGroup();
                    await repository.CreateAsync(group, token);

                    var response = group.MapToResponse();
                    return TypedResults.CreatedAtRoute(response, GetGroupEndpoint.Name, new { group.Id });
                })
                .WithName(Name);
        return app;
    }
}