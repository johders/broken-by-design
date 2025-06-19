using RendezVoulns.Application.Common.Results;
using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Application.Services;

public class UserService : IUserService
{
    public Task<Result> CreateAsync(User user, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> ExistsByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<User>>> GetAllAsync(CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<User>?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<User>?> GetBySlugAsync(string slug, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> SoftDeleteAsync(Guid id, DateTimeOffset deletedOn, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateAsync(User user, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}