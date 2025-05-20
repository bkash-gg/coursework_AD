using AD_Coursework.DTOs.Checkout;
using AD_Coursework.Extensions;
using AD_Coursework.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AD_Coursework.Controllers
{
    [Route("api/checkout")]
    [ApiController]
    [Authorize]
    public class CheckoutController : BaseController
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(
            ICheckoutService checkoutService,
            ILogger<CheckoutController> logger) : base(logger)
        {
            _checkoutService = checkoutService;
        }

        [HttpGet("summary")]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> GetCheckoutSummary()
        {
            try
            {
                var userId = User.GetUserId();
                var summary = await _checkoutService.GetCheckoutSummaryAsync(userId);
                return Success(summary, "Checkout summary retrieved successfully.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, $"Invalid operation while getting checkout summary for user ID: {User.GetUserId()}");
                return Error(ex.Message, StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting checkout summary for user ID: {User.GetUserId()}");
                return HandleException(ex, "We couldn't retrieve your checkout summary. Please try again later.");
            }
        }
    }
}