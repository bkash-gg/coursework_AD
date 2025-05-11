using Microsoft.AspNetCore.Mvc;

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

        private IActionResult BuildResponse(int statusCode, string message, object result, bool isError = false)
        {
            var response = new
            {
                status = statusCode,
                message,
                data = !isError ? result : null,
                errors = isError ? result : null,
            };

            return new ObjectResult(response) { StatusCode = statusCode };
        }

        protected IActionResult SuccessResponse(object data = null, string message = "Operation completed successfully", int statusCode = StatusCodes.Status200OK)
        {
            return BuildResponse(statusCode, message, data);
        }

        protected IActionResult ErrorResponse(string message, int statusCode = StatusCodes.Status400BadRequest, object errors = null)
        {
            _logger.LogError("Error occurred: {Message}", message);
            return BuildResponse(statusCode, message, errors, isError: true);
        }

        protected IActionResult HandleException(Exception ex, string contextMessage = "")
        {
            _logger.LogError(ex, "Exception occurred in Book Library API. Context: {ContextMessage}", contextMessage);
            return BuildResponse(
                StatusCodes.Status500InternalServerError,
                "An unexpected error occurred while processing your request.",
                ex.Message,
                isError: true
            );
        }

        protected IActionResult PaginatedResponse<T>(IEnumerable<T> items, int totalCount, int page, int pageSize)
        {
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var paginationData = new
            {
                Items = items,
                Pagination = new
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItems = totalCount,
                    TotalPages = totalPages,
                    HasPrevious = page > 1,
                    HasNext = page < totalPages
                }
            };

            return SuccessResponse(paginationData);
        }
    }
}
