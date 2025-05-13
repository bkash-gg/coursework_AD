using Microsoft.AspNetCore.Authorization;
using AD_Coursework.Interfaces.Services;
using AD_Coursework.DTOs.Discount;
using Microsoft.AspNetCore.Mvc;

namespace AD_Coursework.Controllers
{
    [Route("api/discounts")]
    [ApiController]
    public class DiscountController : BaseController
    {
        private readonly IDiscountService _discountService;

        // Constructor that initializes the discount service and logger
        public DiscountController(IDiscountService discountService, ILogger<DiscountController> logger)
            : base(logger)
        {
            _discountService = discountService;
        }

        // Retrieves all discounts from the system
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var discounts = await _discountService.GetAllAsync();
                return Success(discounts, "All discounts retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all discounts.");
                return HandleException(ex, "We couldn't retrieve the discount list. Please try again later.");
            }
        }

        // Retrieves a specific discount by its unique identifier
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var discount = await _discountService.GetByIdAsync(id);
                if (discount == null)
                {
                    return Error("We couldn't find the discount you're looking for.", StatusCodes.Status404NotFound);
                }
                return Success(discount, "Discount details retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving discount with ID: {id}");
                return HandleException(ex, "We couldn't retrieve the discount details. Please try again.");
            }
        }

        // Creates a new discount (Admin only)
        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] DiscountCreateDto discountCreateDto)
        {
            try
            {
                if (discountCreateDto == null)
                {
                    return Error("Please provide the discount details.", StatusCodes.Status400BadRequest);
                }

                if (!ModelState.IsValid)
                {
                    return Error("Invalid discount information. Please check your details.", StatusCodes.Status400BadRequest, ModelState);
                }

                var discount = await _discountService.CreateAsync(discountCreateDto);
                return Success(discount, "The discount has been created successfully.", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new discount.");
                return HandleException(ex, "We couldn't create the new discount. Please try again later.");
            }
        }

        // Updates an existing discount (Admin only)
        [HttpPut("{id}/update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] DiscountUpdateDto discountUpdateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Error("Invalid discount information. Please check your details.", StatusCodes.Status400BadRequest, ModelState);
                }

                var discount = await _discountService.UpdateAsync(id, discountUpdateDto);
                return discount == null
                    ? Error("We couldn't find the discount to update.", StatusCodes.Status404NotFound)
                    : Success(discount, "The discount has been updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating discount with ID: {id}");
                return HandleException(ex, "We couldn't update the discount. Please try again later.");
            }
        }

        // Deletes a discount (Admin only)
        [HttpDelete("{id}/delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _discountService.DeleteAsync(id);
                return result
                    ? Success<object>(null, "The discount has been removed successfully.")
                    : Error("We couldn't find the discount to remove.", StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting discount with ID: {id}");
                return HandleException(ex, "We couldn't remove the discount. Please try again later.");
            }
        }

        // Retrieves all currently active discounts
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentDiscounts()
        {
            try
            {
                var discounts = await _discountService.GetCurrentDiscountsAsync();
                return Success(discounts, "Current active discounts retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving current discounts.");
                return HandleException(ex, "We couldn't retrieve the current discounts. Please try again later.");
            }
        }

        // Retrieves the active discount for a specific book
        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetDiscountByBookId(Guid bookId)
        {
            try
            {
                var discount = await _discountService.GetDiscountByBookIdAsync(bookId);
                if (discount == null)
                {
                    return Success<Object>(null, "There are no active discounts for this book currently.");
                }
                return Success(discount, "Book discount retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving discount for book with ID: {bookId}");
                return HandleException(ex, "We couldn't retrieve the discount for this book. Please try again later.");
            }
        }
    }
}