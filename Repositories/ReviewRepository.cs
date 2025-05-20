using AD_Coursework.Interfaces.Repositories;
using AD_Coursework.Models;
using AD_Coursework.Data;
using Microsoft.EntityFrameworkCore;

namespace AD_Coursework.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Book)
                .ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(Guid id)
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Book)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Review> CreateAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<Review?> UpdateAsync(Review review)
        {
            var existingReview = await _context.Reviews.FindAsync(review.Id);
            if (existingReview == null) return null;

            _context.Entry(existingReview).CurrentValues.SetValues(review);
            await _context.SaveChangesAsync();
            return existingReview;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return false;

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Review>> GetReviewsByBookIdAsync(Guid bookId)
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Book)
                .Where(r => r.BookId == bookId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsByUserIdAsync(Guid userId)
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Book)
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> ReviewExistsAsync(Guid id)
        {
            return await _context.Reviews.AnyAsync(r => r.Id == id);
        }

        public async Task<bool> HasUserPurchasedBookAsync(Guid userId, Guid bookId)
        {
            return await _context.OrderItems
                .Include(oi => oi.Order)
                .AnyAsync(oi => oi.BookId == bookId &&
                            oi.Order.UserId == userId);
        }
    }
}
