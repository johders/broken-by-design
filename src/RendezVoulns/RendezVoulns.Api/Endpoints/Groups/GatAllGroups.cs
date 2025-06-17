using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Repositories.Interfaces;

namespace RendezVoulns.Api.Endpoints.Groups;

public static class GetAllGroupsEndpoint
{
    public const string Name = "GetGroups";

    public static IEndpointRouteBuilder MapGetAllGroups(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Groups.GetAll, async (
            IGroupRepository repository, CancellationToken token) =>
                {
                    var groups = await repository.GetAllAsync(token);

                    var response = groups.MapToResponse();
                    return TypedResults.Ok(response);
                })
                .WithName(Name);
        return app;
    }
}