using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Services.Interfaces;
using RendezVoulns.Contracts.V1.User.Requests;

namespace RendezVoulns.Api.Endpoints.Users;

public static class UpdateUserEndpoint
{
    private const string Name = "UpdateUser";

    public static IEndpointRouteBuilder MapUpdateUser(this IEndpointRouteBuilder app)
    {
        app.MapPut(ApiEndpoints.Users.Update, async (
            Guid id, UpdateUserRequest request,
            IUserService service, CancellationToken token) =>
                {
                    var result = await service.GetByIdAsync(id, token)
                        .AndThen(user =>
                        {
                            var userToUpdate = request.MapToUser(user!);
                            return service.UpdateAsync(userToUpdate, token);
                        });

                    return result.Match(
                        onSuccess: updatedUser => TypedResults.Ok(updatedUser.MapToResponse()),
                        onFailure: error => error.ToProblem());  
                })
                .WithName(Name);
        return app;
    }
}