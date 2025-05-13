using AD_Coursework.DTOs.Review;

namespace AD_Coursework.Interfaces.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetAllAsync();
        Task<ReviewDto?> GetByIdAsync(Guid id);
        Task<ReviewDto> CreateAsync(ReviewCreateDto reviewCreateDto);
        Task<ReviewDto?> UpdateAsync(Guid id, Guid userId, ReviewUpdateDto reviewUpdateDto);
        Task<bool> DeleteAsync(Guid id, Guid userId);
        Task<IEnumerable<ReviewDto>> GetReviewsByBookIdAsync(Guid bookId);
        Task<IEnumerable<ReviewDto>> GetReviewsByUserIdAsync(Guid userId);
    }
}