using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Api.Endpoints.Groups;

public static class GetAllGroupsEndpoint
{
    private const string Name = "GetGroups";

    public static IEndpointRouteBuilder MapGetAllGroups(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Groups.GetAll, async (
            IGroupService service, CancellationToken token) =>
                {
                    var result = await service.GetAllAsync(token);

                    return result.Match(
                        onSuccess: groups => TypedResults.Ok(groups.MapToResponse()),
                        onFailure: error => error.ToProblem());
                })
                .WithName(Name);
        return app;
    }
}