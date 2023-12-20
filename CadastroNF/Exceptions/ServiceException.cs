namespace CadastroNF.Exceptions;

public class ServiceException : Exception
{
    public ServiceException(string? message, int statusCode = StatusCodes.Status500InternalServerError) : base(message)
    {
        StatusCode = statusCode;
    }

    public int StatusCode { get; init; }
}