using AD_Coursework.Interfaces.Repositories;
using AD_Coursework.Models;
using AD_Coursework.Data;
using Microsoft.EntityFrameworkCore;

namespace AD_Coursework.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly ApplicationDbContext _context;

        public DiscountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Discount>> GetAllAsync()
        {
            return await _context.Discounts
                .Include(d => d.Book)
                .ToListAsync();
        }

        public async Task<Discount?> GetByIdAsync(Guid id)
        {
            return await _context.Discounts
                .Include(d => d.Book)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Discount> CreateAsync(Discount discount)
        {
            await _context.Discounts.AddAsync(discount);
            await _context.SaveChangesAsync();
            return discount;
        }

        public async Task<Discount?> UpdateAsync(Discount discount)
        {
            var existingDiscount = await _context.Discounts.FindAsync(discount.Id);
            if (existingDiscount == null) return null;

            _context.Entry(existingDiscount).CurrentValues.SetValues(discount);
            await _context.SaveChangesAsync();
            return existingDiscount;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var discount = await _context.Discounts.FindAsync(id);
            if (discount == null) return false;

            _context.Discounts.Remove(discount);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Discount>> GetCurrentDiscountsAsync()
        {
            var now = DateTime.UtcNow;
            return await _context.Discounts
                .Include(d => d.Book)
                .Where(d => d.StartDate <= now && d.EndDate >= now)
                .ToListAsync();
        }

        public async Task<Discount?> GetDiscountByBookIdAsync(Guid bookId)
        {
            var now = DateTime.UtcNow;
            return await _context.Discounts
                .Include(d => d.Book)
                .FirstOrDefaultAsync(d => d.BookId == bookId && d.StartDate <= now && d.EndDate >= now);
        }

        public async Task<bool> DiscountExistsAsync(Guid id)
        {
            return await _context.Discounts.AnyAsync(d => d.Id == id);
        }
    }
}