using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AD_Coursework.Interfaces.Services;
using AD_Coursework.DTOs.Cart;
using AD_Coursework.DTOs.CartItem;
using System.Threading.Tasks;
using AD_Coursework.Extensions;

namespace AD_Coursework.Controllers
{
    [Route("api/cart")]
    [ApiController]
    [Authorize]
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService, ILogger<CartController> logger)
            : base(logger)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            try
            {
                var userId = User.GetUserId();
                var cart = await _cartService.GetCartAsync(userId);
                return Success(cart, "Cart retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to retrieve cart");
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto addToCartDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Error("Invalid data", StatusCodes.Status400BadRequest, ModelState);
                }

                var userId = User.GetUserId();
                var cart = await _cartService.AddToCartAsync(userId, addToCartDto);
                return Success(cart, "Item added to cart successfully.");
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
                return HandleException(ex, "Failed to add item to cart");
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCartItem([FromBody] CartItemUpdateDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Error("Invalid data", StatusCodes.Status400BadRequest, ModelState);
                }

                var userId = User.GetUserId();
                var cart = await _cartService.UpdateCartItemAsync(userId, updateDto);
                return Success(cart, "Cart item updated successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return Error(ex.Message, StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to update cart item");
            }
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFromCart([FromBody] RemoveFromCartDto removeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Error("Invalid data", StatusCodes.Status400BadRequest, ModelState);
                }

                var userId = User.GetUserId();
                var cart = await _cartService.RemoveFromCartAsync(userId, removeDto);
                return Success(cart, "Item removed from cart successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return Error(ex.Message, StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to remove item from cart");
            }
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            try
            {
                var userId = User.GetUserId();
                var success = await _cartService.ClearCartAsync(userId);
                return success
                    ? Success<object>(null, "Cart cleared successfully.")
                    : Error("Cart is already empty", StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to clear cart");
            }
        }
    }
}
