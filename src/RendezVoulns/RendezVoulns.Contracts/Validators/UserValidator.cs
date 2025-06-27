using FluentValidation;
using RendezVoulns.Contracts.V1.User.Requests;

namespace RendezVoulns.Contracts.Validators;

public abstract class UserValidatorBase<T> : AbstractValidator<T> where T : IUserRequest
{
    public UserValidatorBase()
    {
        RuleFor(u => u.Username)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(25)
            .Matches("^[a-zA-Z0-9_]+$").WithMessage("Username can only contain letters, numbers, and underscores");

        RuleFor(u => u.Email)
            .EmailAddress();

        RuleFor(u => u.ProfileImageUrl)
            .Must(url => string.IsNullOrEmpty(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("ProfileImageUrl must be a valid URL");
    }
}

public class CreateUserValidator : UserValidatorBase<CreateUserRequest> { }
public class UpdateUserValidator : UserValidatorBase<UpdateUserRequest> { }
