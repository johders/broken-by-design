using RendezVoulns.Application.Common.Errors;
using RendezVoulns.Application.Common.Exceptions;
using RendezVoulns.Application.Common.Results;
using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Application.Repositories.Interfaces;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Application.Services;

public class TagService(ITagRepository tagRepository, IAppEventRepository appEventRepository) : ITagService
{
    private readonly ITagRepository _tagRepository = tagRepository;
    private readonly IAppEventRepository _appEventRepository = appEventRepository;

    public async Task<Result> CreateAsync(Tag tag, CancellationToken token = default)
    {
        var nameExists = await _tagRepository.NameExistsAsync(tag.Name, token: token);
        if (nameExists)
            return Result.Failure(Errors.Tags.DuplicateNameError);

        try
            {
                var created = await _tagRepository.CreateAsync(tag, token);

                return created
                    ? Result.Success()
                    : Result.Failure(Errors.Tags.CreateFailedError);
            }
            catch (DuplicateException ex)
            {
                return Result.Failure(new Error(ex.Code, ex.Message));
            }
    }

    public async Task<Result<Tag?>> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        var tag = await _tagRepository.GetByIdAsync(id, token);
        return tag is not null
            ? Result<Tag?>.Success(tag)
            : Result<Tag?>.Failure(Errors.Tags.NotFoundError);
    }
    public async Task<Result<IEnumerable<Tag>>> GetAllAsync(CancellationToken token = default)
    {
        var tags = await _tagRepository.GetAllAsync(token);
        return Result<IEnumerable<Tag>>.Success(tags);
    }

    public async Task<Result<Tag>> UpdateAsync(Tag tag, CancellationToken token = default)
    {
        var nameExists = await _tagRepository.NameExistsAsync(tag.Name, tag.Id, token);
        if (nameExists)
            return Result<Tag>.Failure(Errors.Tags.DuplicateNameError);

        var tagExists = await _tagRepository.ExistsByIdAsync(tag.Id);
        if (!tagExists)
            return Result<Tag>.Failure(Errors.Tags.NotFoundError);
            
        try
        {
            var updated = await _tagRepository.UpdateAsync(tag, token);

            return updated
                ? Result<Tag>.Success(tag)
                : Result<Tag>.Failure(Errors.Tags.UpdateFailedError);
        }
        catch (DuplicateException ex)
        {
            return Result<Tag>.Failure(new Error(ex.Code, ex.Message));
        }
    }

    public async Task<Result> SoftDeleteAsync(Guid id, DateTimeOffset deletedOn, CancellationToken token = default)
    {
        var tagExists = await _tagRepository.ExistsByIdAsync(id, token);
        if (!tagExists)
            return Result.Failure(Errors.Tags.NotFoundError);

        var deleted = await _tagRepository.SoftDeleteAsync(id, deletedOn, token);

        return deleted
            ? Result.Success()
            : Result.Failure(Errors.Tags.DeleteFailedError);
    }

    public async Task<Result> DeleteFromEventAsync(Guid eventId, Guid tagId, CancellationToken token = default)
    {

        var tagExists = await _tagRepository.ExistsByIdAsync(tagId, token);
        if (!tagExists)
            return Result.Failure(Errors.Tags.NotFoundError);

        var eventExists = await _appEventRepository.ExistsByIdAsync(eventId, token);
        if (!eventExists)
            return Result.Failure(Errors.AppEvents.NotFoundError);

        var deleted = await _tagRepository.DeleteFromEventAsync(eventId, tagId, token);

        return deleted
            ? Result.Success()
            : Result.Failure(Errors.Tags.DeleteFailedError);
    }

    public async Task<Result> TagEventAsync(Guid eventId, Guid tagId, CancellationToken token = default)
    {

        var tagExists = await _tagRepository.ExistsByIdAsync(tagId, token);
        if (!tagExists)
            return Result.Failure(Errors.Tags.NotFoundError);

        var eventExists = await _appEventRepository.ExistsByIdAsync(eventId, token);
        if (!eventExists)
            return Result.Failure(Errors.AppEvents.NotFoundError);

        var tagged = await _tagRepository.TagEventAsync(eventId, tagId, token);

        return tagged
            ? Result.Success()
            : Result.Failure(Errors.Tags.TagEventFailedError);
    }
}