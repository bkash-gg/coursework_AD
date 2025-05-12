using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AD_Coursework.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected readonly ILogger<BaseController> _logger;

        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private ApiResponse<T> CreateApiResponse<T>(bool success, int statusCode, string message, T data = default, object errors = null)
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

        protected IActionResult Success<T>(T data = default, string message = "Operation completed successfully", int statusCode = StatusCodes.Status200OK)
        {
            var response = CreateApiResponse(true, statusCode, message, data, null);
            return new ObjectResult(response) { StatusCode = statusCode };
        }

        protected IActionResult Error(string message, int statusCode = StatusCodes.Status400BadRequest, object errors = null)
        {
            _logger.LogError("Error response: {StatusCode} - {Message}", statusCode, message);
            var response = CreateApiResponse<object>(false, statusCode, message, null, errors);
            return new ObjectResult(response) { StatusCode = statusCode };
        }

        protected IActionResult HandleException(Exception ex, string userMessage = null)
        {
            _logger.LogError(ex, "Exception caught: {ExceptionType} - {ExceptionMessage}",
                ex.GetType().Name, ex.Message);

            var statusCode = DetermineStatusCode(ex);
            var message = !string.IsNullOrEmpty(userMessage)
                ? userMessage
                : GetUserFriendlyMessage(ex);

            var errorDetails = GetErrorDetails(ex);

            var response = CreateApiResponse<object>(false, statusCode, message, null, errorDetails);
            return new ObjectResult(response) { StatusCode = statusCode };
        }

        private int DetermineStatusCode(Exception ex)
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

        private string GetUserFriendlyMessage(Exception ex)
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

        private object GetErrorDetails(Exception ex)
        {
            var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

            if (isDevelopment)
            {
                return new
                {
                    Type = ex.GetType().Name,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace?.Split('\n'),
                    InnerException = ex.InnerException != null ? new
                    {
                        Type = ex.InnerException.GetType().Name,
                        Message = ex.InnerException.Message
                    } : null
                };
            }

            return null;
        }

        protected IActionResult Paginated<T>(
            IEnumerable<T> items,
            int totalCount,
            int page,
            int pageSize,
            string message = "Data retrieved successfully")
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
