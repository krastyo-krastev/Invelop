using System.Net;
using System.Text.Json;
using ContactsApp.API.Models;
using ContactsApp.Domain.Exceptions;
using FluentValidation;

namespace ContactsApp.API.Middleware;

/// <summary>
/// Middleware for handling exceptions and returning standardized error responses in JSON format.
/// </summary>
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred during request processing");
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var requestId = context.TraceIdentifier;
        ErrorResponse errorResponse;

        switch (exception)
        {
            case DomainException domainEx:
                errorResponse = HandleDomainException(domainEx, requestId);
                break;
            case ValidationException validationEx:
                errorResponse = HandleValidationException(validationEx, requestId);
                break;
            default:
                errorResponse = HandleGenericException(exception, requestId);
                break;
        }

        await WriteErrorResponseAsync(context, errorResponse);
    }

    private ErrorResponse HandleDomainException(DomainException exception, string requestId)
    {
        _logger.LogWarning(exception, "Domain exception occurred");

        return ErrorResponse.DomainError(
            exception.Message,
            requestId
        );
    }

    private ErrorResponse HandleValidationException(ValidationException exception, string requestId)
    {
        _logger.LogWarning(exception, "Validation error occurred");

        return ErrorResponse.ValidationError(
            exception.Message,
            requestId
        );
    }

    private ErrorResponse HandleGenericException(Exception exception, string requestId)
    {
        _logger.LogError(exception, "Unhandled exception occurred");

        return ErrorResponse.Create(
            "INTERNAL_ERROR",
            "An internal error occurred while processing the request",
            (int)HttpStatusCode.InternalServerError,
            requestId
        );
    }

    private async Task WriteErrorResponseAsync(HttpContext context, ErrorResponse errorResponse)
    {
        context.Response.StatusCode = errorResponse.StatusCode;
        context.Response.ContentType = "application/json";

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            WriteIndented = false
        };

        var jsonResponse = JsonSerializer.Serialize(errorResponse, jsonOptions);
        await context.Response.WriteAsync(jsonResponse);
    }
}
