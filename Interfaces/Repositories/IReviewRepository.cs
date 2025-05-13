using AD_Coursework.Models;
using System.Linq.Expressions;

namespace AD_Coursework.Interfaces.Repositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllAsync();
        Task<Review?> GetByIdAsync(Guid id);
        Task<Review> CreateAsync(Review review);
        Task<Review?> UpdateAsync(Review review);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Review>> GetReviewsByBookIdAsync(Guid bookId);
        Task<IEnumerable<Review>> GetReviewsByUserIdAsync(Guid userId);
        Task<bool> ReviewExistsAsync(Guid id);
        Task<bool> HasUserPurchasedBookAsync(Guid userId, Guid bookId);
    }
}
