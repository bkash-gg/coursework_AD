using AD_Coursework.Models;
using System.Linq.Expressions;

namespace AD_Coursework.Interfaces.Repositories
{
    public interface IAnnouncementRepository
    {
        Task<Announcement?> GetByIdAsync(Guid id);
        Task<IEnumerable<Announcement>> GetAllAsync();
        Task<IEnumerable<Announcement>> FindAsync(Expression<Func<Announcement, bool>> predicate);
        Task<Announcement> AddAsync(Announcement entity);
        Task<Announcement> UpdateAsync(Announcement entity);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Announcement>> GetActiveAnnouncementsAsync();
    }
}