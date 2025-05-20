using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AD_Coursework.Interfaces.Services;
using AD_Coursework.Extensions;

namespace AD_Coursework.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
            : base(logger)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> PlaceOrder()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Error("Invalid order information.", StatusCodes.Status400BadRequest, ModelState);
                }

                var userId = User.GetUserId();
                var order = await _orderService.PlaceOrderAsync(userId);
                return Success(order, "Your order has been placed successfully.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, $"Invalid operation while placing order for user ID: {User.GetUserId()}");
                return Error(ex.Message, StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while placing order for user ID: {User.GetUserId()}");
                return HandleException(ex, "We couldn't place your order. Please try again later.");
            }
        }

        [HttpGet("{orderId}")]
        [Authorize]
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            try
            {
                var userId = User.GetUserId();
                var order = await _orderService.GetOrderByIdAsync(userId, orderId);
                return Success(order, "Order retrieved successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Order not found for user ID: {User.GetUserId()}");
                return Error("Order not found.", StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving order for user ID: {User.GetUserId()}");
                return HandleException(ex, "We couldn't retrieve your order. Please try again later.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserOrders()
        {
            try
            {
                var userId = User.GetUserId();
                var orders = await _orderService.GetUserOrdersAsync(userId);
                return Success(orders, "Your orders have been retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving orders for user ID: {User.GetUserId()}");
                return HandleException(ex, "We couldn't retrieve your orders. Please try again later.");
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllOrdersCount()
        {
            try
            {
                var orderCount = await _orderService.GetOrderCount();
                return Success(orderCount, "All orders has been retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving orders for user ID: {User.GetUserId()}");
                return HandleException(ex, "We couldn't retrieve your orders. Please try again later.");
            }
        }

        [HttpPost("{orderId}/cancel")]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> CancelOrder(Guid orderId)
        {
            try
            {
                var userId = User.GetUserId();
                var order = await _orderService.CancelOrderAsync(userId, orderId);
                return Success(order, "Your order has been cancelled successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Order not found while cancelling for user ID: {User.GetUserId()}");
                return Error("Order not found.", StatusCodes.Status404NotFound);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, $"Invalid operation while cancelling order for user ID: {User.GetUserId()}");
                return Error(ex.Message, StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while cancelling order for user ID: {User.GetUserId()}");
                return HandleException(ex, "We couldn't cancel your order. Please try again later.");
            }
        }

        [HttpPost("{orderId}/placeagain")]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> PlaceOrderAgain(Guid orderId)
        {
            try
            {
                var userId = User.GetUserId();
                var order = await _orderService.PlaceOrderAgain(userId, orderId);
                return Success<Object>(null, "Your order has been cancelled successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Order not found while cancelling for user ID: {User.GetUserId()}");
                return Error("Order not found.", StatusCodes.Status404NotFound);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, $"Invalid operation while cancelling order for user ID: {User.GetUserId()}");
                return Error(ex.Message, StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while cancelling order for user ID: {User.GetUserId()}");
                return HandleException(ex, "We couldn't cancel your order. Please try again later.");
            }
        }

        [HttpPost("process")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> ProcessOrder([FromQuery] Guid userId, [FromQuery] string claimCode)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(claimCode))
                {
                    return Error("Claim code is required.", StatusCodes.Status400BadRequest);
                }

                var order = await _orderService.ProcessOrderAsync(userId, claimCode);
                return Success(order, "Order processed successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Order not found while processing for user ID: {userId}");
                return Error("Order not found with the provided claim code.", StatusCodes.Status404NotFound);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, $"Invalid operation while processing order for user ID: {userId}");
                return Error(ex.Message, StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while processing order for user ID: {userId}");
                return HandleException(ex, "We couldn't process this order. Please try again later.");
            }
        }

        [HttpGet("all-orders")]
        [Authorize(Roles = "Staff,Admin")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrdersAsync();
                return Success(orders, "All orders retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all orders");
                return HandleException(ex, "We couldn't retrieve all orders. Please try again later.");
            }
        }

        [HttpPost("complete")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> CompleteOrder([FromQuery] Guid userId, [FromQuery] string claimCode)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(claimCode))
                {
                    return Error("Claim code is required.", StatusCodes.Status400BadRequest);
                }

                var order = await _orderService.CompleteOrderAsync(userId, claimCode);
                return Success(order, "Order completed successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Order not found while completing for user ID: {userId}");
                return Error("Order not found with the provided claim code.", StatusCodes.Status404NotFound);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, $"Invalid operation while completing order for user ID: {userId}");
                return Error(ex.Message, StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while completing order for user ID: {userId}");
                return HandleException(ex, "We couldn't complete this order. Please try again later.");
            }
        }
    }
}