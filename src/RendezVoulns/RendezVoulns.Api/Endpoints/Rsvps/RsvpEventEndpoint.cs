using RendezVoulns.Api.Mapping;
using RendezVoulns.Api.Validation;
using RendezVoulns.Application.Services.Interfaces;
using RendezVoulns.Contracts.V1.Rsvp.Requests;

namespace RendezVoulns.Api.Endpoints.Rsvps;

public static class RsvpEventEndpoint
{
    private const string Name = "RsvpEvent";

    public static IEndpointRouteBuilder MapRsvpEvent(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.AppEvents.Rsvp, async (
            Guid id, RsvpEventRequest request, IRsvpService service,
            CancellationToken token) =>
                {
                    var userId = Guid.Parse("00000000-0000-0000-0000-000000000003");
                    var rsvp = id.MapToRsvp(userId, request.Status);

                    var result = await service.RsvpEventAsync(rsvp, token);

                    return result.Match(
                        onSuccess: () => TypedResults.Ok(),
                        onFailure: error => error.ToProblem());
                })
                .WithName(Name)
                .WithValidation<RsvpEventRequest>();
        return app;
    }
}