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

        public DiscountController(IDiscountService discountService, ILogger<DiscountController> logger)
            : base(logger)
        {
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var discounts = await _discountService.GetAllAsync();
                return Success(discounts, "Discounts retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to retrieve discounts");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var discount = await _discountService.GetByIdAsync(id);
                if (discount == null)
                {
                    return Error("Discount not found.", StatusCodes.Status404NotFound);
                }
                return Success(discount, "Discount retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to retrieve discount");
            }
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] DiscountCreateDto discountCreateDto)
        {
            try
            {
                if (discountCreateDto == null)
                {
                    return Error("Request body cannot be empty.", StatusCodes.Status400BadRequest);
                }

                if (!ModelState.IsValid)
                {
                    return Error("Invalid data. Please check your details.", StatusCodes.Status400BadRequest, ModelState);
                }

                var discount = await _discountService.CreateAsync(discountCreateDto);
                return Success(discount, "Discount created successfully", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to create discount");
            }
        }

        [HttpPut("{id}/update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] DiscountUpdateDto discountUpdateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Error("Invalid data", StatusCodes.Status400BadRequest, ModelState);
                }

                var discount = await _discountService.UpdateAsync(id, discountUpdateDto);
                return discount == null
                    ? Error("Discount not found.", StatusCodes.Status404NotFound)
                    : Success(discount, "Discount updated successfully");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to update discount");
            }
        }

        [HttpDelete("{id}/delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _discountService.DeleteAsync(id);
                return result
                    ? Success<object>(null, "Discount deleted successfully")
                    : Error("Discount not found.", StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to delete discount");
            }
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentDiscounts()
        {
            try
            {
                var discounts = await _discountService.GetCurrentDiscountsAsync();
                return Success(discounts, "Current discounts retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to retrieve current discounts");
            }
        }

        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetDiscountByBookId(Guid bookId)
        {
            try
            {
                var discount = await _discountService.GetDiscountByBookIdAsync(bookId);
                if (discount == null)
                {
                    return Success<Object>(null, "No active discount found for this book.");
                }
                return Success(discount, "Discount retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to retrieve discount for book");
            }
        }
    }
}