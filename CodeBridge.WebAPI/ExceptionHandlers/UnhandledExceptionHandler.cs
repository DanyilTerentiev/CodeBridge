using CodeBridge.Domain.Models;
using ExceptionHandler;

namespace CodeBridge.WebAPI.ExceptionHandlers;

public class UnhandledExceptionHandler : IExceptionHandler<Exception>
{
    public async Task ProceedAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync(exception.Message);
    }
}