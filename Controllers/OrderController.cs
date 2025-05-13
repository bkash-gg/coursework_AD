using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AD_Coursework.Interfaces.Services;
using AD_Coursework.DTOs.Order;
using AD_Coursework.Extensions;

namespace AD_Coursework.Controllers
{
    [Route("api/orders")]
    [ApiController]
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
            : base(logger)
        {
            _orderService = orderService;
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(orderId);
                return Success(order, "Order retrieved successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return Error(ex.Message, StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to retrieve order");
            }
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserOrders()
        {
            try
            {
                var userId = User.GetUserId();
                var orders = await _orderService.GetUserOrdersAsync(userId);
                return Success(orders, "User orders retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to retrieve user orders");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto orderCreateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Error("Invalid data", StatusCodes.Status400BadRequest, ModelState);
                }

                var userId = User.GetUserId();
                orderCreateDto.UserId = userId; 

                var order = await _orderService.CreateOrderAsync(orderCreateDto);
                return Success(order, "Order created successfully.", StatusCodes.Status201Created);
            }
            catch (KeyNotFoundException ex)
            {
                return Error(ex.Message, StatusCodes.Status404NotFound);
            }
            catch (InvalidOperationException ex)
            {
                return Error(ex.Message, StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to create order");
            }
        }

        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(Guid orderId, [FromBody] OrderUpdateDto orderUpdateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Error("Invalid data", StatusCodes.Status400BadRequest, ModelState);
                }

                var order = await _orderService.UpdateOrderAsync(orderId, orderUpdateDto);
                return Success(order, "Order updated successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return Error(ex.Message, StatusCodes.Status404NotFound);
            }
            catch (InvalidOperationException ex)
            {
                return Error(ex.Message, StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to update order");
            }
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> CancelOrder(Guid orderId, [FromQuery] string cancellationReason)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cancellationReason))
                {
                    return Error("Cancellation reason is required", StatusCodes.Status400BadRequest);
                }

                var success = await _orderService.CancelOrderAsync(orderId, cancellationReason);
                return success
                    ? Success<object>(null, "Order cancelled successfully.")
                    : Error("Failed to cancel order", StatusCodes.Status400BadRequest);
            }
            catch (KeyNotFoundException ex)
            {
                return Error(ex.Message, StatusCodes.Status404NotFound);
            }
            catch (InvalidOperationException ex)
            {
                return Error(ex.Message, StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to cancel order");
            }
        }
    }
}