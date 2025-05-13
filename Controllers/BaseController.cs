using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AD_Coursework.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected readonly ILogger<BaseController> _logger;

        // Constructor that initializes the logger for the controller
        public BaseController(ILogger<BaseController> logger) => _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        // Private helper method to create standardized API response objects
        private static ApiResponse<T> CreateApiResponse<T>(bool success, int statusCode, string message, T? data = default, object? errors = null)
        {
            return new ApiResponse<T>
            {
                Success = success,
                StatusCode = statusCode,
                Message = message,
                Data = data,
                Errors = errors,
                Timestamp = DateTime.UtcNow
            };
        }

        // Returns a success response with the specified data and message
        protected IActionResult Success<T>(T? data = default, string message = "Operation completed successfully", int statusCode = StatusCodes.Status200OK)
        {
            var response = CreateApiResponse(true, statusCode, message, data, null);
            return new ObjectResult(response) { StatusCode = statusCode };
        }

        // Returns an error response with the specified message and status code
        protected IActionResult Error(string message, int statusCode = StatusCodes.Status400BadRequest, object? errors = null)
        {
            _logger.LogError("Returning error response. StatusCode: {StatusCode}, Message: {Message}", statusCode, message);
            var response = CreateApiResponse<object>(false, statusCode, message, null, errors);
            return new ObjectResult(response) { StatusCode = statusCode };
        }

        // Handles exceptions by logging them and returning an appropriate error response
        protected IActionResult HandleException(Exception ex, string? userMessage = null)
        {
            var statusCode = DetermineStatusCode(ex);
            var message = !string.IsNullOrEmpty(userMessage)
                ? userMessage
                : GetUserFriendlyMessage(ex);

            var errorDetails = GetErrorDetails(ex);

            _logger.LogError(ex, "Exception occurred. StatusCode: {StatusCode}, Message: {Message}, Exception: {ExceptionType}", statusCode, message, ex.GetType().Name);

            var response = CreateApiResponse<object>(false, statusCode, message, null, errorDetails);
            return new ObjectResult(response) { StatusCode = statusCode };
        }

        // Determines the appropriate HTTP status code based on the exception type
        private static int DetermineStatusCode(Exception ex)
        {
            return ex switch
            {
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                ArgumentException => StatusCodes.Status400BadRequest,
                InvalidOperationException => StatusCodes.Status400BadRequest,
                JsonException => StatusCodes.Status400BadRequest,
                NotImplementedException => StatusCodes.Status501NotImplemented,
                TimeoutException => StatusCodes.Status504GatewayTimeout,
                _ => StatusCodes.Status500InternalServerError
            };
        }

        // Provides a user-friendly message based on the exception type
        private static string GetUserFriendlyMessage(Exception ex)
        {
            return ex switch
            {
                UnauthorizedAccessException => "You don't have permission to perform this action",
                KeyNotFoundException => "The requested resource was not found",
                ArgumentException => "Invalid data provided",
                InvalidOperationException => "This operation cannot be completed at this time",
                JsonException => "Invalid data format",
                TimeoutException => "The operation timed out. Please try again later",
                _ => "An unexpected error occurred"
            };
        }

        // Returns detailed error information in development environment only
        private static object GetErrorDetails(Exception ex)
        {
            var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

            if (isDevelopment)
            {
                return new
                {
                    Type = ex.GetType().Name,
                    ex.Message,
                    StackTrace = ex.StackTrace?.Split('\n'),
                    InnerException = ex.InnerException != null ? new
                    {
                        Type = ex.InnerException.GetType().Name,
                        ex.InnerException.Message
                    } : null
                };
            }

            return null!;
        }

        // Returns a paginated response with metadata for the client to handle pagination
        protected IActionResult Paginated<T>(IEnumerable<T> items, int totalCount, int page, int pageSize, string message = "Data retrieved successfully")
        {
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var paginationMetadata = new
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = totalCount,
                TotalPages = totalPages,
                HasPrevious = page > 1,
                HasNext = page < totalPages
            };

            var result = new
            {
                Items = items,
                Metadata = paginationMetadata
            };

            return Success(result, message);
        }
    }

    // Generic API response model used for standardizing API output
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        public int StatusCode { get; set; }

        public string? Message { get; set; }

        public T? Data { get; set; }

        public object? Errors { get; set; }

        public DateTime Timestamp { get; set; }
    }
}