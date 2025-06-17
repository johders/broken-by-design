using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Repositories.Interfaces;

namespace RendezVoulns.Api.Endpoints.Users;

public static class GetUserEndpoint
{
    public const string Name = "GetUser";

    public static IEndpointRouteBuilder MapGetUser(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Users.Get, async (
            string idOrSlug, IUserRepository repository,
            CancellationToken token) =>
                {
                    var user = Guid.TryParse(idOrSlug, out var id)
                        ? await repository.GetByIdAsync(id, token)
                        : await repository.GetBySlugAsync(idOrSlug, token);

                    if (user is null)
                        return Results.NotFound();

                    var response = user.MapToResponse();
                    return TypedResults.Ok(response);
                })
                .WithName(Name);
        return app;
    }
}