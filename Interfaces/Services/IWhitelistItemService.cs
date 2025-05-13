using AD_Coursework.DTOs.WhitelistItem;

namespace AD_Coursework.Interfaces.Services
{
    public interface IWhitelistItemService
    {
        Task<IEnumerable<WhitelistItemDto>> GetWhitelistItemsByUserIdAsync(Guid userId);
        Task<WhitelistItemDto?> GetWhitelistItemByIdAsync(Guid id);
        Task<WhitelistItemDto> CreateWhitelistItemAsync(WhitelistItemCreateDto whitelistItemCreateDto);
        Task<bool> DeleteWhitelistItemAsync(Guid id);
        Task<bool> WhitelistItemExistsAsync(Guid userId, Guid bookId);
        Task<int> GetWhitelistItemCountForUserAsync(Guid userId);
    }
}
