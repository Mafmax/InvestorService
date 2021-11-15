using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using Mafmax.InvestorService.Api.Extensions;
using Mafmax.InvestorService.Services.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Mafmax.InvestorService.Api.Middleware;

/// <summary>
/// Middleware to handle exceptions coming from controllers
/// </summary>
public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    /// <summary>
    /// Middleware constructor
    /// </summary>
    /// <param name="logger"></param>
    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,"Error: {error}",ex);

            await HandleExceptionAsync(context, ex);
        }
    }

    private async static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = GetStatusCode(exception);

        var response = new {
            title = GetTitle(exception),
            status = code,
            detail = exception.Message,
            errors = GetErrors(exception)
        };

        context.Response.ContentType = "application/json";

        context.Response.StatusCode = code;

        await context.Response.WriteAsJsonAsync(response);
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch {
            EntityNotFoundException => StatusCodes.Status404NotFound,
            InvalidOperationException => StatusCodes.Status400BadRequest,
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };

    private static string GetTitle(Exception exception) =>
        exception switch {
            _ => "Server error"
        };

    private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
    {
        IReadOnlyDictionary<string, string[]> errors = null;

        if (exception is ValidationException validationException) 
            errors = validationException.ErrorsDictionary();

        return errors;
    }
}
