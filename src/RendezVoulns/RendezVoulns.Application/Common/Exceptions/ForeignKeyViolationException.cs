namespace RendezVoulns.Application.Common.Exceptions;

public class ForeignKeyViolationException(string code, string message) : ServiceException(code, message)
{
    
}