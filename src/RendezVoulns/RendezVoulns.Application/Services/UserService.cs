using RendezVoulns.Application.Common.Errors;
using RendezVoulns.Application.Common.Exceptions;
using RendezVoulns.Application.Common.Results;
using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Application.Repositories.Interfaces;
using RendezVoulns.Application.Services.Interfaces;

namespace RendezVoulns.Application.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result> CreateAsync(User user, CancellationToken token = default)
    {
        try
        {
            var created = await _userRepository.CreateAsync(user, token);

            return created
                ? Result.Success()
                : Result.Failure(Errors.Users.CreateFailedError);
        }
        catch (DuplicateException ex)
        {
            return Result.Failure(new Error(ex.Code, ex.Message));
        }
        catch (ForeignKeyViolationException ex)
        {
            return Result.Failure(new Error(ex.Code, ex.Message));
        }
    }

    public async Task<Result<User?>> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        var user = await _userRepository.GetByIdAsync(id, token);
        return user is not null
            ? Result<User?>.Success(user) 
            : Result<User?>.Failure(Errors.Users.NotFoundError);
    }

    public async Task<Result<User?>> GetBySlugAsync(string slug, CancellationToken token = default)
    {
        var user = await _userRepository.GetBySlugAsync(slug, token);

        return user is not null
            ? Result<User?>.Success(user)
            : Result<User?>.Failure(Errors.Users.NotFoundError);
    }

    public async Task<Result<IEnumerable<User>>> GetAllAsync(CancellationToken token = default)
    {
        var users = await _userRepository.GetAllAsync(token);
        return Result<IEnumerable<User>>.Success(users);
    }

    public async Task<Result<User>> UpdateAsync(User user, CancellationToken token = default)
    {
        try
        {
            var updated = await _userRepository.UpdateAsync(user, token);

            return updated
                ? Result<User>.Success(user)
                : Result<User>.Failure(Errors.Users.UpdateFailedError);
        }
        catch (DuplicateException ex)
        {
            return Result<User>.Failure(new Error(ex.Code, ex.Message));
        }
        catch (ForeignKeyViolationException ex)
        {
            return Result<User>.Failure(new Error(ex.Code, ex.Message));
        }
    }

    public async Task<Result> SoftDeleteAsync(Guid id, DateTimeOffset deletedOn, CancellationToken token = default)
    {
        var deleted = await _userRepository.SoftDeleteAsync(id, deletedOn, token);

        return deleted
            ? Result.Success()
            : Result.Failure(Errors.Users.DeleteFailedError);
    }

    public Task<Result> ExistsByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}