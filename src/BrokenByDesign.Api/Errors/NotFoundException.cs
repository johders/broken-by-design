namespace BrokenByDesign.Api.Errors;

public class NotFoundException(string message) : ServiceException(StatusCodes.Status404NotFound, message)
{
    
}