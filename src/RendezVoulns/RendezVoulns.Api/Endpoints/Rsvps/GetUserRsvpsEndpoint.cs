using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Api.Endpoints.Rsvps;

public static class GetAllTagsEndpoint
{
    private const string Name = "GetUserRsvps";

    public static IEndpointRouteBuilder MapGetUserRsvps(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Rsvps.GetUserRsvps, async (
            IRsvpService service, CancellationToken token) =>
                {
                    var userId = Guid.Parse("00000000-0000-0000-0000-000000000003");
                    var result = await service.GetRsvpsForUserAsync(userId, token);

                    return result.Match(
                        onSuccess: rsvps => TypedResults.Ok(rsvps.MapToResponse()),
                        onFailure: error => error.ToProblem());
                })
                .WithName(Name);
        return app;
    }
}