using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Repositories.Interfaces;

namespace RendezVoulns.Api.Endpoints.Users;

public static class GetAllUsersEndpoint
{
    public const string Name = "GetUsers";

    public static IEndpointRouteBuilder MapGetAllUsers(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Users.GetAll, async (
            IUserRepository repository, CancellationToken token) =>
                {
                    var users = await repository.GetAllAsync(token);

                    var response = users.MapToResponse();
                    return TypedResults.Ok(response);
                })
                .WithName(Name);
        return app;
    }
}