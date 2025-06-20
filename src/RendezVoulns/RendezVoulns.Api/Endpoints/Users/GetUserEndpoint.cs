using RendezVoulns.Api.Mapping;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Api.Endpoints.Users;

public static class GetUserEndpoint
{
    public const string Name = "GetUser";

    public static IEndpointRouteBuilder MapGetUser(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Users.Get, async (
            string idOrSlug, IUserService service,
            CancellationToken token) =>
                {
                    var result = Guid.TryParse(idOrSlug, out var id)
                        ? await service.GetByIdAsync(id, token)
                        : await service.GetBySlugAsync(idOrSlug, token);

                    return result.Match(
                        onSuccess: user => TypedResults.Ok(user!.MapToResponse()),
                        onFailure: error => error.ToProblem());
                })
                .WithName(Name);
        return app;
    }
}