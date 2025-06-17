using RendezVoulns.Application.Repositories.Interfaces;

namespace RendezVoulns.Api.Endpoints.Users;

public static class DeleteUserEndpoint
{
    public const string Name = "DeleteUser";

    public static IEndpointRouteBuilder MapDeleteUser(this IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiEndpoints.Users.Delete, async (
            Guid id, IUserRepository repository,
            CancellationToken token) =>
            {
                var user = await repository.GetByIdAsync(id, token);

                if (user is null)
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