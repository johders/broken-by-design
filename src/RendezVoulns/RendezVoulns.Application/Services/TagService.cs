using RendezVoulns.Application.Common.Results;
using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Application.Services;

public class TagService : ITagService
{
    public Task<Result> CreateAsync(Tag tag, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<Tag>>> GetAllAsync(CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Tag?>> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> SoftDeleteAsync(Guid id, DateTimeOffset deletedOn, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateAsync(Tag tag, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}