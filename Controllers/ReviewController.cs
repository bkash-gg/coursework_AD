using Microsoft.AspNetCore.Authorization;
using AD_Coursework.Interfaces.Services;
using AD_Coursework.DTOs.Review;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AD_Coursework.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : BaseController
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService, ILogger<ReviewController> logger)
            : base(logger)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var reviews = await _reviewService.GetAllAsync();
                return Success(reviews, "Reviews retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to retrieve reviews");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var review = await _reviewService.GetByIdAsync(id);
                if (review == null)
                {
                    return Error("Review not found.", StatusCodes.Status404NotFound);
                }
                return Success(review, "Review retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to retrieve review");
            }
        }

        [HttpPost("add")]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Create([FromBody] ReviewCreateDto reviewCreateDto)
        {
            try
            {
                if (reviewCreateDto == null)
                {
                    return Error("Request body cannot be empty.", StatusCodes.Status400BadRequest);
                }

                if (!ModelState.IsValid)
                {
                    return Error("Invalid data. Please check your details.", StatusCodes.Status400BadRequest, ModelState);
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null || !Guid.TryParse(userId, out var parsedUserId))
                {
                    return Error("Invalid user identity.", StatusCodes.Status401Unauthorized);
                }

                if (reviewCreateDto.UserId != parsedUserId)
                {
                    return Error("You can only create reviews for yourself.", StatusCodes.Status403Forbidden);
                }

                var review = await _reviewService.CreateAsync(reviewCreateDto);
                return Success(review, "Review created successfully", StatusCodes.Status201Created);
            }
            catch (ArgumentException ex)
            {
                return Error(ex.Message, StatusCodes.Status400BadRequest);
            }
            catch (InvalidOperationException ex)
            {
                // This will catch the "User must purchase the book before reviewing" exception
                return Error(ex.Message, StatusCodes.Status403Forbidden);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to create review");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ReviewUpdateDto reviewUpdateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Error("Invalid data", StatusCodes.Status400BadRequest, ModelState);
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null || !Guid.TryParse(userId, out var parsedUserId))
                {
                    return Error("Invalid user identity.", StatusCodes.Status401Unauthorized);
                }

                var review = await _reviewService.UpdateAsync(id, parsedUserId, reviewUpdateDto);
                return review == null
                    ? Error("Review not found.", StatusCodes.Status404NotFound)
                    : Success(review, "Review updated successfully");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Error(ex.Message, StatusCodes.Status403Forbidden);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to update review");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null || !Guid.TryParse(userId, out var parsedUserId))
                {
                    return Error("Invalid user identity.", StatusCodes.Status401Unauthorized);
                }

                var result = await _reviewService.DeleteAsync(id, parsedUserId);
                return result
                    ? Success<object>(null, "Review deleted successfully")
                    : Error("Review not found.", StatusCodes.Status404NotFound);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Error(ex.Message, StatusCodes.Status403Forbidden);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to delete review");
            }
        }

        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetReviewsByBookId(Guid bookId)
        {
            try
            {
                var reviews = await _reviewService.GetReviewsByBookIdAsync(bookId);
                return Success(reviews, "Reviews retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to retrieve reviews for book");
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetReviewsByUserId(Guid userId)
        {
            try
            {
                var reviews = await _reviewService.GetReviewsByUserIdAsync(userId);
                return Success(reviews, "Reviews retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to retrieve reviews by user");
            }
        }
    }
}
