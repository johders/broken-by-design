using RendezVoulns.Application.Common.Results;
using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Application.Services;

public class GroupService : IGroupService
{
    public Task<Result> CreateAsync(Group group, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> ExistsByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<Group>>> GetAllAsync(CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Group?>> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> SoftDeleteAsync(Guid id, DateTimeOffset deletedOn, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateAsync(Group group, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}