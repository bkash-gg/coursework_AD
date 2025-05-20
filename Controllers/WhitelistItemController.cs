using Microsoft.AspNetCore.Authorization;
using AD_Coursework.Interfaces.Services;
using AD_Coursework.DTOs.WhitelistItem;
using Microsoft.AspNetCore.Mvc;
using AD_Coursework.Extensions;

namespace AD_Coursework.Controllers
{
    [Route("api/whitelist")]
    [ApiController]
    [Authorize]
    public class WhitelistItemController : BaseController
    {
        private readonly IWhitelistItemService _whitelistItemService;

        public WhitelistItemController(IWhitelistItemService whitelistItemService, ILogger<WhitelistItemController> logger)
            : base(logger)
        {
            _whitelistItemService = whitelistItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWhitelistItemsByUserId()
        {
            try
            {
                var userId = User.GetUserId();
                var whitelistItems = await _whitelistItemService.GetWhitelistItemsByUserIdAsync(userId);
                return Success(whitelistItems, "Whitelist items retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to retrieve whitelist items");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateWhitelistItem([FromBody] WhitelistItemCreateDto whitelistItemCreateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Error("Invalid data", StatusCodes.Status400BadRequest, ModelState);
                }

                var whitelistItem = await _whitelistItemService.CreateWhitelistItemAsync(whitelistItemCreateDto);
                return Success(whitelistItem, "Book added to whitelist successfully", StatusCodes.Status201Created);
            }
            catch (ArgumentException ex)
            {
                return Error(ex.Message, StatusCodes.Status404NotFound);
            }
            catch (InvalidOperationException ex)
            {
                return Error(ex.Message, StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to add book to whitelist");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWhitelistItem(Guid id)
        {
            try
            {
                var result = await _whitelistItemService.DeleteWhitelistItemAsync(id);
                if (!result)
                {
                    return Error("Whitelist item not found.", StatusCodes.Status404NotFound);
                }
                return Success<object>(null, "Book removed from whitelist successfully");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to remove book from whitelist");
            }
        }

        [HttpGet("count/{userId}")]
        public async Task<IActionResult> GetWhitelistItemCountForUser(Guid userId)
        {
            try
            {
                var count = await _whitelistItemService.GetWhitelistItemCountForUserAsync(userId);
                return Success(count, "Whitelist item count retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to retrieve whitelist item count");
            }
        }
    }
}