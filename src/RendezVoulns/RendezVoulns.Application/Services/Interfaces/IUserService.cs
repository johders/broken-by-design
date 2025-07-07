using RendezVoulns.Application.Common.Results;
using RendezVoulns.Application.Models.Entities;

namespace RendezVoulns.Application.Services.Interfaces;

public interface IUserService
{
    Task<Result> CreateAsync(User user, CancellationToken token = default);
    Task<Result<User?>> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<Result<User?>> GetBySlugAsync(string slug, CancellationToken token = default);
    Task<Result<IEnumerable<User>>> GetAllAsync(CancellationToken token = default);
    Task<Result<User>> UpdateAsync(User user, CancellationToken token = default);
    Task<Result> SoftDeleteAsync(Guid id, DateTimeOffset deletedOn, CancellationToken token = default);
}