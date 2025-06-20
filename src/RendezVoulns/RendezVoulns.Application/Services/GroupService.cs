using RendezVoulns.Application.Common.Errors;
using RendezVoulns.Application.Common.Exceptions;
using RendezVoulns.Application.Common.Results;
using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Application.Repositories.Interfaces;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Application.Services;

public class GroupService(IGroupRepository groupRepository) : IGroupService
{
    private readonly IGroupRepository _groupRepository = groupRepository;
    public async Task<Result> CreateAsync(Group group, CancellationToken token = default)
    {
        try
        {
            var created = await _groupRepository.CreateAsync(group, token);

            return created
                ? Result.Success()
                : Result.Failure(Errors.Groups.CreateFailedError);
        }
        catch (DuplicateException ex)
        {
            return Result.Failure(new Error(ex.Code, ex.Message));
        }
    }

    public async Task<Result<Group?>> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        var group = await _groupRepository.GetByIdAsync(id, token);
        return group is not null
            ? Result<Group?>.Success(group) 
            : Result<Group?>.Failure(Errors.Groups.NotFoundError);
    }

    public async Task<Result<IEnumerable<Group>>> GetAllAsync(CancellationToken token = default)
    {
        var groups = await _groupRepository.GetAllAsync(token);
        return Result<IEnumerable<Group>>.Success(groups);
    }

    public async Task<Result> UpdateAsync(Group group, CancellationToken token = default)
    {
        try
        {
            var updated = await _groupRepository.UpdateAsync(group, token);

            return updated
                ? Result<Group>.Success(group)
                : Result<Group>.Failure(Errors.Groups.UpdateFailedError);
        }
        catch (DuplicateException ex)
        {
            return Result<Group>.Failure(new Error(ex.Code, ex.Message));
        }
    }

    public async Task<Result> SoftDeleteAsync(Guid id, DateTimeOffset deletedOn, CancellationToken token = default)
    {
        var deleted = await _groupRepository.SoftDeleteAsync(id, deletedOn, token);

        return deleted
            ? Result.Success()
            : Result.Failure(Errors.Groups.DeleteFailedError);
    }

    public Task<Result> ExistsByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}