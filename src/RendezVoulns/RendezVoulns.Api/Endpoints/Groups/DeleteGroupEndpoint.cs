using RendezVoulns.Application.Repositories.Interfaces;

namespace RendezVoulns.Api.Endpoints.Groups;

public static class DeleteGroupEndpoint
{
    public const string Name = "DeleteGroup";

    public static IEndpointRouteBuilder MapDeleteGroup(this IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiEndpoints.Groups.Delete, async (
            Guid id, IGroupRepository repository,
            CancellationToken token) =>
            {
                var group = await repository.GetByIdAsync(id, token);

                if (group is null)
                    return Results.NotFound();

                bool deleted = await repository.SoftDeleteAsync(id, DateTimeOffset.UtcNow, token);

                return deleted
                ? TypedResults.Ok()
                : Results.StatusCode(StatusCodes.Status500InternalServerError);
            })
            .WithName(Name);
        return app;
    }
}