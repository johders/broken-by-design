using FluentValidation;
using RendezVoulns.Contracts.V1.Tag.Requests;

namespace RendezVoulns.Contracts.Validators;

public abstract class TagValidatorBase<T> : AbstractValidator<T> where T : ITagRequest
{
    public TagValidatorBase()
    {
        RuleFor(t => t.Name)
            .NotEmpty()
            .MaximumLength(30);
            
        RuleFor(t => t.ColorHex)
            .NotEmpty()
            .Matches(@"^#[0-9a-fA-F]{6}$")
            .WithMessage("ColorHex must be a valid 6-digit hex color (e.g. #FFAA33)");
    }
}

public class CreateTagValidator : TagValidatorBase<CreateTagRequest> { }
public class UpdateTagValidator : TagValidatorBase<UpdateTagRequest> { }