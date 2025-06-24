using FluentValidation;
using RendezVoulns.Contracts.V1.AppEvent.Requests;

namespace RendezVoulns.Contracts.Validators;

public class AppEventValidator : AbstractValidator<CreateAppEventRequest>
{
    public AppEventValidator()
    {
        RuleFor(e => e.StartTime)
            .GreaterThanOrEqualTo(DateTimeOffset.Now)
            .WithMessage("Start time cannot be in the past");

        RuleFor(e => e.EndTime)
            .GreaterThanOrEqualTo(e => e.StartTime)
            .WithMessage("End time must be after start time");
    }
}