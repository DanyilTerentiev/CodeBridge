﻿using CodeBridge.Domain.Exceptions;
using CodeBridge.Domain.Models;
using ExceptionHandler;

namespace CodeBridge.WebAPI.ExceptionHandlers;

public class ValidationExceptionHandler : IExceptionHandler<ValidationException>
{
    public async Task ProceedAsync(HttpContext context, ValidationException exception)
    {
        context.Response.StatusCode = exception.StatusCode;
        await context.Response.WriteAsJsonAsync(exception.Error);
    }
}