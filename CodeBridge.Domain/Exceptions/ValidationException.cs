using ExceptionHandler;

namespace CodeBridge.Domain.Exceptions;

public class ValidationException : Exception, IAppException
{
    public ValidationException(IEnumerable<AppError> error)
    {
        Error = error;
    }
    
    public int StatusCode => 400;
    
    public IEnumerable<AppError>? Error { get; }
}