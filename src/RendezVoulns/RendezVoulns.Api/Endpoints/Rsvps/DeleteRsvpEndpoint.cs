using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Api.Endpoints.Rsvps;

public static class DeleteRsvpEndpoint
{
    private const string Name = "DeleteRsvp";

    public static IEndpointRouteBuilder MapDeleteRsvp(this IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiEndpoints.AppEvents.DeleteRsvp, async (
            Guid id, IRsvpService service,
            CancellationToken token) =>
            {
                var userId = Guid.Parse("00000000-0000-0000-0000-000000000003");
                var result = await service.GetByIdAsync(id, userId, token)
                    .AndThen(rsvp => service.SoftDeleteAsync(id, userId, token));

                return result.Match(
                    onSuccess: () => TypedResults.Ok(),
                    onFailure: error => error.ToProblem()
                );
            })
            .WithName(Name);
        return app;
    }
}