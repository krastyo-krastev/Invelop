using System.Text.Json.Serialization;

namespace ContactsApp.API.Models;

/// <summary>
/// Represents a standardized error response for API endpoints
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Error code for programmatic handling
    /// </summary>
    [JsonPropertyName("error_code")]
    public string ErrorCode { get; set; } = string.Empty;

    /// <summary>
    /// Human-readable error message
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp when the error occurred
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Request ID for tracing and debugging
    /// </summary>
    [JsonPropertyName("request_id")]
    public string? RequestId { get; set; }

    /// <summary>
    /// Additional error details (optional)
    /// </summary>
    [JsonPropertyName("details")]
    public Dictionary<string, object>? Details { get; set; }

    /// <summary>
    /// HTTP status code
    /// </summary>
    [JsonPropertyName("status_code")]
    public int StatusCode { get; set; }

    /// <summary>
    /// Create a standard error response
    /// </summary>
    public static ErrorResponse Create(
        string errorCode,
        string message,
        int statusCode = 500,
        string? requestId = null,
        Dictionary<string, object>? details = null)
    {
        return new ErrorResponse
        {
            ErrorCode = errorCode,
            Message = message,
            StatusCode = statusCode,
            RequestId = requestId,
            Details = details,
            Timestamp = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Create error response for validation failures
    /// </summary>
    public static ErrorResponse ValidationError(string message, string? requestId = null, Dictionary<string, object>? details = null)
    {
        return Create("VALIDATION_ERROR", message, 400, requestId, details);
    }
}
