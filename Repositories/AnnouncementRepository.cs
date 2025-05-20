using AD_Coursework.Data;
using AD_Coursework.Interfaces.Repositories;
using AD_Coursework.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AD_Coursework.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly ApplicationDbContext _context;

        public AnnouncementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Announcement?> GetByIdAsync(Guid id)
        {
            return await _context.Announcements.FindAsync(id);
        }

        public async Task<IEnumerable<Announcement>> GetAllAsync()
        {
            return await _context.Announcements.ToListAsync();
        }

        public async Task<IEnumerable<Announcement>> FindAsync(Expression<Func<Announcement, bool>> predicate)
        {
            return await _context.Announcements.Where(predicate).ToListAsync();
        }

        public async Task<Announcement> AddAsync(Announcement entity)
        {
            await _context.Announcements.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Announcement> UpdateAsync(Announcement entity)
        {
            _context.Announcements.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var announcement = await GetByIdAsync(id);
            if (announcement == null) return false;

            _context.Announcements.Remove(announcement);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Announcement>> GetActiveAnnouncementsAsync()
        {
            var now = DateTime.UtcNow;
            return await _context.Announcements
                .Where(a => a.IsActive && a.StartDate <= now && a.EndDate >= now)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }
    }
}