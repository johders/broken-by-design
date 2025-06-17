using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Repositories.Interfaces;
using RendezVoulns.Contracts.V1.Requests;

namespace RendezVoulns.Api.Endpoints.Tags;

public static class UpdateTagEndpoint
{
    public const string Name = "UpdateTag";

    public static IEndpointRouteBuilder MapUpdateTag(this IEndpointRouteBuilder app)
    {
        app.MapPut(ApiEndpoints.Tags.Update, async (
            Guid id, UpdateTagRequest request,
            ITagRepository repository, CancellationToken token) =>
                {
                    var tag = await repository.GetByIdAsync(id, token);

                    if (tag is null)
                        return Results.NotFound();

                    var updatedTag = request.MapToTag(tag);
                    await repository.UpdateAsync(updatedTag, token);

                    var response = updatedTag.MapToResponse();
                    return TypedResults.Ok(response);
                })
                .WithName(Name);
        return app;
    }
}