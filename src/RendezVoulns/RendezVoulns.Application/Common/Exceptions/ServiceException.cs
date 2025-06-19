namespace RendezVoulns.Application.Common.Exceptions;
public abstract class ServiceException(string code, string message) : Exception(message)
{
    public string Code { get; } = code;
}