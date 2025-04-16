using Microsoft.AspNetCore.Mvc;
using System;

namespace AD_Coursework.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly ILogger<BaseController> _logger;

        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
        }

        protected IActionResult HandleException(Exception ex, string contextMessage = "")
        {
            _logger.LogError(ex, $"Exception occurred. Context: {contextMessage}");
            return StatusCode(500, new { message = "An unexpected error occurred.", detail = ex.Message });
        }

        protected IActionResult FormatResponse(object data, string message = "Success")
        {
            return Ok(new
            {
                status = 200,
                message,
                data
            });
        }
    }
}