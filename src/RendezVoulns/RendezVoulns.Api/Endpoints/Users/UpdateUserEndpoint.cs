using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Repositories.Interfaces;
using RendezVoulns.Contracts.V1.User.Requests;

namespace RendezVoulns.Api.Endpoints.Users;

public static class UpdateUserEndpoint
{
    public const string Name = "UpdateUser";

    public static IEndpointRouteBuilder MapUpdateUser(this IEndpointRouteBuilder app)
    {
        app.MapPut(ApiEndpoints.Users.Update, async (
            Guid id, UpdateUserRequest request,
            IUserRepository repository, CancellationToken token) =>
                {
                    var user = await repository.GetByIdAsync(id, token);

                    if (user is null)
                        return Results.NotFound();

                    var updatedUser = request.MapToUser(user);
                    await repository.UpdateAsync(updatedUser, token);

                    var response = updatedUser.MapToResponse();
                    return TypedResults.Ok(response);
                })
                .WithName(Name);
        return app;
    }
}