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
    [Authorize(Roles = "Member")]
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;

        // Constructor that initializes the cart service and logger
        public CartController(ICartService cartService, ILogger<CartController> logger)
            : base(logger)
        {
            _cartService = cartService;
        }

        // Retrieves the current user's shopping cart
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            try
            {
                var userId = User.GetUserId();
                var cart = await _cartService.GetCartAsync(userId);
                return Success(cart, "Your shopping cart has been retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving cart for user ID: {User.GetUserId()}");
                return HandleException(ex, "We couldn't retrieve your shopping cart. Please try again later.");
            }
        }

        // Adds an item to the user's shopping cart
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto addToCartDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Error("Invalid item information. Please check your details.", StatusCodes.Status400BadRequest, ModelState);
                }

                var userId = User.GetUserId();
                var cart = await _cartService.AddToCartAsync(userId, addToCartDto);
                return Success(cart, "The item has been added to your cart successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Item not found while adding to cart for user ID: {User.GetUserId()}");
                return Error("We couldn't find the item you're trying to add.", StatusCodes.Status404NotFound);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, $"Invalid operation while adding to cart for user ID: {User.GetUserId()}");
                return Error(ex.Message, StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while adding item to cart for user ID: {User.GetUserId()}");
                return HandleException(ex, "We couldn't add this item to your cart. Please try again later.");
            }
        }

        // Updates an item in the user's shopping cart
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCartItem([FromBody] CartItemUpdateDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Error("Invalid update information. Please check your details.", StatusCodes.Status400BadRequest, ModelState);
                }

                var userId = User.GetUserId();
                var cart = await _cartService.UpdateCartItemAsync(userId, updateDto);
                return Success(cart, "Your cart item has been updated successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Cart item not found while updating for user ID: {User.GetUserId()}");
                return Error("We couldn't find the item in your cart to update.", StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating cart item for user ID: {User.GetUserId()}");
                return HandleException(ex, "We couldn't update this item in your cart. Please try again later.");
            }
        }

        // Removes an item from the user's shopping cart
        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFromCart([FromBody] RemoveFromCartDto removeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Error("Invalid removal request. Please check your details.", StatusCodes.Status400BadRequest, ModelState);
                }

                var userId = User.GetUserId();
                var cart = await _cartService.RemoveFromCartAsync(userId, removeDto);
                return Success(cart, "The item has been removed from your cart successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Cart item not found while removing for user ID: {User.GetUserId()}");
                return Error("We couldn't find the item in your cart to remove.", StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while removing item from cart for user ID: {User.GetUserId()}");
                return HandleException(ex, "We couldn't remove this item from your cart. Please try again later.");
            }
        }

        // Clears all items from the user's shopping cart
        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            try
            {
                var userId = User.GetUserId();
                var success = await _cartService.ClearCartAsync(userId);
                return success
                    ? Success<object>(null, "Your shopping cart has been cleared successfully.")
                    : Error("Your cart is already empty.", StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while clearing cart for user ID: {User.GetUserId()}");
                return HandleException(ex, "We couldn't clear your shopping cart. Please try again later.");
            }
        }
    }
}