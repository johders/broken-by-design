using FluentValidation;
using RendezVoulns.Contracts.V1.Group.Requests;

namespace RendezVoulns.Contracts.Validators;

public abstract class GroupValidatorBase<T> : AbstractValidator<T> where T : IGroupRequest
{
    public GroupValidatorBase()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(e => e.Description)
            .MaximumLength(1000);
    }
}

public class CreateGroupValidator : GroupValidatorBase<CreateGroupRequest> { }
public class UpdateGroupValidator : GroupValidatorBase<UpdateGroupRequest> { }