using RendezVoulns.Application.Common.Errors;

namespace RendezVoulns.Api.Validation;

public static class Errors
{
    public static class Validation
    {
        public static Error InvalidRequest(Dictionary<string, string[]> failures) =>
            new("Validation.InvalidRequest", "One or more validation errors occurred")
            {
                Extensions = new Dictionary<string, object>
                {
                    {"errors", failures}
                }
            };
    }
}