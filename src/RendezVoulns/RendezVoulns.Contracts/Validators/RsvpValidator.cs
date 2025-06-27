using System.Data;
using FluentValidation;
using RendezVoulns.Contracts.V1.Rsvp.Requests;

namespace RendezVoulns.Contracts.Validators;

public class RsvpValidator : AbstractValidator<RsvpEventRequest>
{
    public static readonly string[] AllowedStatuses = ["Yes", "No", "Maybe"];
    
    public RsvpValidator()
    {
        RuleFor(r => r.Status)
            .NotEmpty()
            .Must(s => AllowedStatuses.Contains(s, StringComparer.OrdinalIgnoreCase))
            .WithMessage($"Status must be one of these: {string.Join(", ", AllowedStatuses)}");
    }
}