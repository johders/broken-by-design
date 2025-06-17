using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Repositories.Interfaces;
using RendezVoulns.Contracts.V1.Requests;

namespace RendezVoulns.Api.Endpoints.Users;

public static class CreateUseEndpoint
{
    public const string Name = "CreateUser";

    public static IEndpointRouteBuilder MapCreateUser(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.Users.Create, async (
            CreateUserRequest request, IUserRepository repository, CancellationToken token) =>
                {
                    var user = request.MapToUser();
                    await repository.CreateAsync(user, token);

                    var response = user.MapToResponse();
                    return TypedResults.Ok(response);
                    //return TypedResults.CreatedAtRoute(response, GetUserEndpoint.Name, new { user.Id });
                })
                .WithName(Name);
        return app;
    }
}