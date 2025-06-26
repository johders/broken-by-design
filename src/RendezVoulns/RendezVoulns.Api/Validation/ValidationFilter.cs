
using FluentValidation;
using RendezVoulns.Api.Mapping;

namespace RendezVoulns.Api.Validation;

public class ValidationFilter<T>(IValidator<T> validator) : IEndpointFilter where T : class
{
    private readonly IValidator<T> _validator = validator;

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var argument = context.Arguments.FirstOrDefault(arg => arg is T) as T;
        var validationResult = await _validator.ValidateAsync(argument!);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray());

            var error = Errors.Validation.InvalidRequest(errors);
            return error.ToProblem();
        }

        return await next(context);
    }
}