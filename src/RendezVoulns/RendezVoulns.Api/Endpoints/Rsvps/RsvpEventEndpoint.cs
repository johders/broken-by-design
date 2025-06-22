using RendezVoulns.Application.Services.Interfaces;
using RendezVoulns.Contracts.V1.Rsvp.Requests;

namespace RendezVoulns.Api.Endpoints.Rsvps;

public static class RsvpEventEndpoint
{
    private const string Name = "RsvpEvent";

    public static IEndpointRouteBuilder MapRsvpEvent(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.AppEvents.Create, async (
            Guid id, RsvpEventRequest request, IRsvpService service,
            CancellationToken token) =>
                {
                    // TODO
                })
                .WithName(Name);
        return app;
    }
}