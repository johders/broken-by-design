using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Services.Interfaces;
using RendezVoulns.Contracts.V1.User.Requests;

namespace RendezVoulns.Api.Endpoints.Users;

public static class CreateUseEndpoint
{
    private const string Name = "CreateUser";

    public static IEndpointRouteBuilder MapCreateUser(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.Users.Create, async (
            CreateUserRequest request, IUserService service, CancellationToken token) =>
                {
                    var user = request.MapToUser();
                    var result = await service.CreateAsync(user, token);

                    return result.Match(
                        onSuccess: () => TypedResults.CreatedAtRoute(user.MapToResponse(), GetUserEndpoint.Name, new { idOrSlug = user.Slug }),
                        onFailure: error => error.ToProblem());
                })
                .WithName(Name);
        return app;
    }
}