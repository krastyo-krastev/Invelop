using System.Net;
using System.Text.Json;
using ContactsApp.API.Models;

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
            case HttpRequestException httpEx:
                errorResponse = HandleHttpRequestException(httpEx, requestId);
                break;
            case TaskCanceledException taskEx when taskEx.InnerException is TimeoutException:
                errorResponse = HandleTimeoutException(taskEx, requestId);
                break;
            case TimeoutException timeoutEx:
                errorResponse = HandleTimeoutException(timeoutEx, requestId);
                break;
            case ArgumentException argEx:
                errorResponse = HandleArgumentException(argEx, requestId);
                break;
            default:
                errorResponse = HandleGenericException(exception, requestId);
                break;
        }

        await WriteErrorResponseAsync(context, errorResponse);
    }

    private ErrorResponse HandleHttpRequestException(HttpRequestException exception, string requestId)
    {
        _logger.LogWarning(exception, "HTTP request exception occurred during proxy operation");

        // Determine appropriate status code based on the exception message or inner exception
        var statusCode = HttpStatusCode.ServiceUnavailable; // Default to 503
        var errorCode = "BACKEND_ERROR";

        if (exception.Message.Contains("timeout", StringComparison.OrdinalIgnoreCase))
        {
            statusCode = HttpStatusCode.GatewayTimeout;
            errorCode = "BACKEND_TIMEOUT";
        }
        else if (exception.Message.Contains("connection", StringComparison.OrdinalIgnoreCase))
        {
            errorCode = "BACKEND_CONNECTION_ERROR";
        }

        return ErrorResponse.Create(
            errorCode,
            "Backend service is temporarily unavailable",
            (int)statusCode,
            requestId
        );
    }

    private ErrorResponse HandleTimeoutException(Exception exception, string requestId)
    {
        _logger.LogWarning(exception, "Request timeout occurred");

        return ErrorResponse.Create(
            "REQUEST_TIMEOUT",
            "The request took too long to complete",
            (int)HttpStatusCode.GatewayTimeout,
            requestId
        );
    }

    private ErrorResponse HandleArgumentException(ArgumentException exception, string requestId)
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
