using RendezVoulns.Application.Common.Results;
using RendezVoulns.Application.Models.Entities;

namespace RendezVoulns.Application.Services.Interfaces;

public interface ITagService
{
    Task<Result> CreateAsync(Tag tag, CancellationToken token = default);
    Task<Result<Tag?>> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<Result<IEnumerable<Tag>>> GetAllAsync(CancellationToken token = default);
    Task<Result> UpdateAsync(Tag tag, CancellationToken token = default);
    Task<Result> SoftDeleteAsync(Guid id, DateTimeOffset deletedOn, CancellationToken token = default);
}