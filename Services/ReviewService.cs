using AD_Coursework.DTOs.Review;
using AD_Coursework.Interfaces.Repositories;
using AD_Coursework.Interfaces.Services;
using AD_Coursework.Models;
using AutoMapper;

namespace AD_Coursework.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;

        public ReviewService(
            IReviewRepository reviewRepository,
            IUserRepository userRepository,
            IBookRepository bookRepository)
        {
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<ReviewDto>> GetAllAsync()
        {
            var reviews = await _reviewRepository.GetAllAsync();

            var reviewDtos = reviews.Select(r => new ReviewDto
            {
                Id = r.Id,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt,
                UserId = r.UserId,
                BookId = r.BookId
            });

            return reviewDtos;
        }

        public async Task<ReviewDto?> GetByIdAsync(Guid id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);

            if (review == null)
                return null;

            var reviewDto = new ReviewDto
            {
                Id = review.Id,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt,
                UserId = review.UserId,
                BookId = review.BookId
            };

            return reviewDto;
        }

        public async Task<ReviewDto> CreateAsync(ReviewCreateDto reviewCreateDto)
        {
            var user = await _userRepository.GetUserByIdAsync(reviewCreateDto.UserId);
            if (user == null)
            {
                throw new ArgumentException("Invalid User ID.");
            }

            var book = await _bookRepository.GetBookByIdAsync(reviewCreateDto.BookId);
            if (book == null)
            {
                throw new ArgumentException("Invalid Book ID.");
            }

            // Check if user has purchased the book
            var hasPurchased = await _reviewRepository.HasUserPurchasedBookAsync(reviewCreateDto.UserId, reviewCreateDto.BookId);
            if (!hasPurchased)
            {
                throw new InvalidOperationException("You must purchase this book before submitting a review.");
            }

            var review = new Review
            {
                Rating = reviewCreateDto.Rating,
                Comment = reviewCreateDto.Comment,
                UserId = reviewCreateDto.UserId,
                BookId = reviewCreateDto.BookId,
                CreatedAt = DateTime.UtcNow
            };

            var createdReview = await _reviewRepository.CreateAsync(review);

            var reviewDto = new ReviewDto
            {
                Id = createdReview.Id,
                Rating = createdReview.Rating,
                Comment = createdReview.Comment,
                CreatedAt = createdReview.CreatedAt,
                UserId = createdReview.UserId,
                BookId = createdReview.BookId
            };

            return reviewDto;
        }

        public async Task<ReviewDto?> UpdateAsync(Guid id, Guid userId, ReviewUpdateDto reviewUpdateDto)
        {
            var existingReview = await _reviewRepository.GetByIdAsync(id);
            if (existingReview == null) return null;

            if (existingReview.UserId != userId)
            {
                throw new UnauthorizedAccessException("You can only update your own reviews.");
            }

            if (reviewUpdateDto.Rating.HasValue)
            {
                existingReview.Rating = reviewUpdateDto.Rating.Value;
            }

            if (reviewUpdateDto.Comment != null)
            {
                existingReview.Comment = reviewUpdateDto.Comment;
            }

            var updatedReview = await _reviewRepository.UpdateAsync(existingReview);

            var reviewDto = new ReviewDto
            {
                Id = updatedReview.Id,
                Rating = updatedReview.Rating,
                Comment = updatedReview.Comment,
                CreatedAt = updatedReview.CreatedAt,
                UserId = updatedReview.UserId,
                BookId = updatedReview.BookId
            };

            return reviewDto;
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null) return false;

            if (review.UserId != userId)
            {
                throw new UnauthorizedAccessException("You can only delete your own reviews.");
            }

            return await _reviewRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsByBookIdAsync(Guid bookId)
        {
            var reviews = await _reviewRepository.GetReviewsByBookIdAsync(bookId);

            var reviewDtos = reviews.Select(review => new ReviewDto
            {
                Id = review.Id,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt,
                UserId = review.UserId,
                BookId = review.BookId
            });

            return reviewDtos;
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsByUserIdAsync(Guid userId)
        {
            var reviews = await _reviewRepository.GetReviewsByUserIdAsync(userId);

            var reviewDtos = reviews.Select(review => new ReviewDto
            {
                Id = review.Id,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt,
                UserId = review.UserId,
                BookId = review.BookId
            });

            return reviewDtos;
        }
    }
}
