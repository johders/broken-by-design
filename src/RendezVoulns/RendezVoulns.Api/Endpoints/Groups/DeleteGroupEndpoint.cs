using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Api.Endpoints.Groups;

public static class DeleteGroupEndpoint
{
    private const string Name = "DeleteGroup";

    public static IEndpointRouteBuilder MapDeleteGroup(this IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiEndpoints.Groups.Delete, async (
            Guid id, IGroupService service,
            CancellationToken token) =>
            {
                var result = await service.GetByIdAsync(id, token)
                    .AndThen(group => service.SoftDeleteAsync(group!.Id, DateTimeOffset.UtcNow, token));

                return result.Match(
                    onSuccess: () => TypedResults.Ok(),
                    onFailure: error => error.ToProblem()
                );
            })
            .WithName(Name);
        return app;
    }
}