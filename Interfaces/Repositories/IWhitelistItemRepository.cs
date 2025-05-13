using AD_Coursework.Models;
using System.Linq.Expressions;

namespace AD_Coursework.Interfaces.Repositories
{
    public interface IWhitelistItemRepository
    {
        Task<IEnumerable<WhitelistItem>> GetWhitelistItemsByUserIdAsync(Guid userId);
        Task<WhitelistItem?> GetWhitelistItemByIdAsync(Guid id);
        Task<WhitelistItem> CreateWhitelistItemAsync(WhitelistItem whitelistItem);
        Task<bool> DeleteWhitelistItemAsync(Guid id);
        Task<bool> WhitelistItemExistsAsync(Guid userId, Guid bookId);
        Task<int> GetWhitelistItemCountForUserAsync(Guid userId);
    }
}
