using RendezVoulns.Application.Models.Entities;

namespace RendezVoulns.Application.Repositories.Interfaces;

public interface IUserRepository
{
    Task<bool> CreateAsync(User user, CancellationToken token = default);
    Task<User?> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<User?> GetBySlugAsync(string slug, CancellationToken token = default);
    Task<IEnumerable<User>> GetAllAsync(CancellationToken token = default);
    Task<bool> UpdateAsync(User user, CancellationToken token = default);
    Task<bool> SoftDeleteAsync(Guid id, DateTimeOffset deletedOn, CancellationToken token = default);
    Task<bool> ExistsByIdAsync(Guid id, CancellationToken token = default);
    Task<bool> UsernameExistsAsync(string username, Guid? excludeId = null, CancellationToken token = default);
    Task<bool> EmailExistsAsync(string email, Guid? excludeId = null, CancellationToken token = default);
    Task<bool> SlugExistsAsync(string slug, Guid? excludeId = null, CancellationToken token = default);
}