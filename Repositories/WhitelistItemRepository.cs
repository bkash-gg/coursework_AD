using AD_Coursework.Interfaces.Repositories;
using AD_Coursework.Models;
using AD_Coursework.Data;
using Microsoft.EntityFrameworkCore;

namespace AD_Coursework.Repositories
{
    public class WhitelistItemRepository : IWhitelistItemRepository
    {
        private readonly ApplicationDbContext _context;

        public WhitelistItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WhitelistItem>> GetWhitelistItemsByUserIdAsync(Guid userId)
        {
            return await _context.WhitelistedItems
                .Include(wi => wi.Book)
                    .ThenInclude(b => b.Author)
                .Include(wi => wi.Book)
                    .ThenInclude(b => b.Publisher)
                .Include(wi => wi.Book)
                    .ThenInclude(b => b.BookGenres)
                        .ThenInclude(bg => bg.Genre)
                .Where(wi => wi.UserId == userId)
                .OrderByDescending(wi => wi.AddedOn)
                .ToListAsync();
        }

        public async Task<WhitelistItem?> GetWhitelistItemByIdAsync(Guid id)
        {
            return await _context.WhitelistedItems
                .Include(wi => wi.Book)
                .Include(wi => wi.User)
                .FirstOrDefaultAsync(wi => wi.Id == id);
        }

        public async Task<WhitelistItem> CreateWhitelistItemAsync(WhitelistItem whitelistItem)
        {
            await _context.WhitelistedItems.AddAsync(whitelistItem);
            await _context.SaveChangesAsync();
            return whitelistItem;
        }

        public async Task<bool> DeleteWhitelistItemAsync(Guid id)
        {
            var whitelistItem = await _context.WhitelistedItems.FindAsync(id);
            if (whitelistItem == null) return false;

            _context.WhitelistedItems.Remove(whitelistItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> WhitelistItemExistsAsync(Guid userId, Guid bookId)
        {
            return await _context.WhitelistedItems
                .AnyAsync(wi => wi.UserId == userId && wi.BookId == bookId);
        }

        public async Task<int> GetWhitelistItemCountForUserAsync(Guid userId)
        {
            return await _context.WhitelistedItems
                .Where(wi => wi.UserId == userId)
                .CountAsync();
        }
    }
}