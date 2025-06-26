using FluentValidation;
using RendezVoulns.Contracts.V1.AppEvent.Requests;

namespace RendezVoulns.Contracts.Validators;

public class AppEventValidatorBase<T> : AbstractValidator<T> where T : IAppEventRequest
{
    public AppEventValidatorBase()
    {
        RuleFor(e => e.Title)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(e => e.Description)
            .MaximumLength(1000);

        RuleFor(e => e.Location)
            .MaximumLength(255);

        RuleFor(e => e.GroupId)
            .NotEmpty();

        RuleFor(e => e.StartTime)
            .GreaterThanOrEqualTo(DateTimeOffset.Now)
            .WithMessage("Start time cannot be in the past")
            .LessThanOrEqualTo(DateTimeOffset.UtcNow.AddYears(2))
            .WithMessage("Start time cannot be more than 2 years in the future");

        RuleFor(e => e.EndTime)
            .GreaterThanOrEqualTo(e => e.StartTime)
            .WithMessage("End time must be after start time");

        RuleFor(e => e)
            .Must(e => (e.EndTime - e.StartTime).TotalDays <= 7)
            .WithMessage("Event duration cannot exceed 7 days");
    }
}

public class CreateAppEventValidator : AppEventValidatorBase<CreateAppEventRequest> { }

public class UpdateAppEventValidator : AppEventValidatorBase<UpdateAppEventRequest> { }
