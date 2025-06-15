namespace RendezVoulns.Application.Errors.Postgresql;

public class DuplicateSlugException(string message) : Exception(message)
{

}

