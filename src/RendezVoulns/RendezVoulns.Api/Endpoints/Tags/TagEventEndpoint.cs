using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Services.Interfaces;
using RendezVoulns.Contracts.V1.Tag.Requests;

namespace RendezVoulns.Api.Endpoints.Tags;

public static class TagEventEndpoint
{
    private const string Name = "TagEvent";

    public static IEndpointRouteBuilder MapTagEvent(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.AppEvents.Tag, async (
            Guid id, TagEventRequest request, ITagService service, CancellationToken token) =>
                {
                    var result = await service.GetByIdAsync(request.TagId, token)
                    .AndThen(tag => service.TagEventAsync(id, tag!.Id, token));

                    return result.Match(
                        onSuccess: () => TypedResults.Ok(),
                        onFailure: error => error.ToProblem());
                })
                .WithName(Name);
        return app;
    }
}