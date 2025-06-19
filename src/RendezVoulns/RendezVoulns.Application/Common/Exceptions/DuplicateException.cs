namespace RendezVoulns.Application.Common.Exceptions;

public class DuplicateException(string code, string message) : ServiceException(code, message)
{
    
}

