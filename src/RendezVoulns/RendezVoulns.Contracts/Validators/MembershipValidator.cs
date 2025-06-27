using FluentValidation;
using RendezVoulns.Contracts.V1.Membership.Requests;

namespace RendezVoulns.Contracts.Validators;

public class MembershipValidator : AbstractValidator<UpdateMembershipRequest>
{
    public static readonly string[] AllowedRoles = ["Member", "Organizer", "Admin"];
    public MembershipValidator()
    {
        RuleFor(m => m.UserId)
            .NotEmpty();

        RuleFor(m => m.Role)
            .NotEmpty()
            .Must(r => AllowedRoles.Contains(r, StringComparer.OrdinalIgnoreCase))
            .WithMessage($"Role must be one of these: {string.Join(", ", AllowedRoles)}");
    }
}