using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Api.Endpoints.Groups;

public static class GetGroupEndpoint
{
    public const string Name = "GetGroup";

    public static IEndpointRouteBuilder MapGetGroup(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Groups.Get, async (
            Guid id, IGroupService service, CancellationToken token) =>
                {
                    var result = await service.GetByIdAsync(id, token);

                    return result.Match(
                        onSuccess: group => TypedResults.Ok(group!.MapToResponse()),
                        onFailure: error => error.ToProblem());
                })
                .WithName(Name);
        return app;
    }
}