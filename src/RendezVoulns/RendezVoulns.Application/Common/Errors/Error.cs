namespace RendezVoulns.Application.Common.Errors;

public record Error(string Code, string Message)
{
    public static readonly Error None = new("None", string.Empty);
    public string Domain => Code.Split('.')[0];

    public Dictionary<string, object>? Extensions { get; init; }
}
