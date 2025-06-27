using RendezVoulns.Api.Mapping;
using RendezVoulns.Api.Validation;
using RendezVoulns.Application.Services.Interfaces;
using RendezVoulns.Contracts.V1.Tag.Requests;

namespace RendezVoulns.Api.Endpoints.Tags;

public static class CreateTagEndpoint
{
    private const string Name = "CreateTag";

    public static IEndpointRouteBuilder MapCreateTag(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.Tags.Create, async (
            CreateTagRequest request, ITagService service, CancellationToken token) =>
                {
                    var tag = request.MapToTag();
                    var result = await service.CreateAsync(tag, token);

                    return result.Match(
                        onSuccess: () => TypedResults.CreatedAtRoute(tag.MapToResponse(), GetTagEndpoint.Name, new { tag.Id }),
                        onFailure: error => error.ToProblem());
                })
                .WithName(Name)
                .WithValidation<CreateTagRequest>();
        return app;
    }
}