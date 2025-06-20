using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Api.Endpoints.Users;

public static class DeleteUserEndpoint
{
    private const string Name = "DeleteUser";

    public static IEndpointRouteBuilder MapDeleteUser(this IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiEndpoints.Users.Delete, async (
            Guid id, IUserService service,
            CancellationToken token) =>
            {
                var result = await service.GetByIdAsync(id, token)
                    .AndThen(user => service.SoftDeleteAsync(user!.Id, DateTimeOffset.UtcNow, token));

                return result.Match(
                    onSuccess: () => TypedResults.Ok(),
                    onFailure: error => error.ToProblem());
            })
            .WithName(Name);
        return app;
    }
}