using AD_Coursework.DTOs.Announcement;
using AD_Coursework.Models;

namespace AD_Coursework.Interfaces.Services
{
    public interface IAnnouncementService
    {
        Task<IEnumerable<AnnouncementDto>> GetAllAnnouncementsAsync();
        Task<AnnouncementDto?> GetAnnouncementByIdAsync(Guid id);
        Task<AnnouncementDto> CreateAnnouncementAsync(AnnouncementCreateDto createDto);
        Task<AnnouncementDto?> UpdateAnnouncementAsync(Guid id, AnnouncementUpdateDto updateDto);
        Task<bool> DeleteAnnouncementAsync(Guid id);
        Task<IEnumerable<AnnouncementDto>> GetActiveAnnouncementsAsync();
        Task BroadcastAnnouncementAsync(Guid announcementId);
    }
}