namespace RendezVoulns.Application.Errors.Postgresql;

public class ForeignKeyViolationException(string message) : Exception(message)
{
    
}