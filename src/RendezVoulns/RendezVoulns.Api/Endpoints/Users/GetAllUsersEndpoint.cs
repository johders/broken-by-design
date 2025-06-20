using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Api.Endpoints.Users;

public static class GetAllUsersEndpoint
{
    private const string Name = "GetUsers";

    public static IEndpointRouteBuilder MapGetAllUsers(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Users.GetAll, async (
            IUserService service, CancellationToken token) =>
                {
                    var result = await service.GetAllAsync(token);

                    return result.Match(
                        onSuccess: users => TypedResults.Ok(users.MapToResponse()),
                        onFailure: error => error.ToProblem());
                })
                .WithName(Name);
        return app;
    }
}