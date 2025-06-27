using RendezVoulns.Api.Mapping;
using RendezVoulns.Api.Validation;
using RendezVoulns.Application.Services.Interfaces;
using RendezVoulns.Contracts.V1.Tag.Requests;

namespace RendezVoulns.Api.Endpoints.Tags;

public static class UpdateTagEndpoint
{
    private const string Name = "UpdateTag";

    public static IEndpointRouteBuilder MapUpdateTag(this IEndpointRouteBuilder app)
    {
        app.MapPut(ApiEndpoints.Tags.Update, async (
            Guid id, UpdateTagRequest request,
            ITagService service, CancellationToken token) =>
                {
                    var result = await service.GetByIdAsync(id, token)
                        .AndThen(tag =>
                        {
                            var tagToUpdate = request.MapToTag(tag!);
                            return service.UpdateAsync(tagToUpdate, token);
                        });

                    return result.Match(
                        onSuccess: updatedTag => TypedResults.Ok(updatedTag.MapToResponse()),
                        onFailure: error => error.ToProblem());
                })
                .WithName(Name)
                .WithValidation<UpdateTagRequest>();
        return app;
    }
}