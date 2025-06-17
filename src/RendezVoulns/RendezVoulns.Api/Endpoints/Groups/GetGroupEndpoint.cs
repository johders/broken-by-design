using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Repositories.Interfaces;

namespace RendezVoulns.Api.Endpoints.Groups;

public static class GetGroupEndpoint
{
    public const string Name = "GetGroup";

    public static IEndpointRouteBuilder MapGetGroup(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Groups.Get, async (
            Guid id, IGroupRepository repository, CancellationToken token) =>
                {
                    var group = await repository.GetByIdAsync(id, token);

                    if (group is null)
                        return Results.NotFound();

                    var response = group.MapToResponse();
                    return TypedResults.Ok(response);
                })
                .WithName(Name);
        return app;
    }
}