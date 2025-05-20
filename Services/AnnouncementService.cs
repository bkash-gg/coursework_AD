using AD_Coursework.DTOs.Announcement;
using AD_Coursework.Interfaces.Repositories;
using AD_Coursework.Interfaces.Services;
using AD_Coursework.Models;
using Microsoft.AspNetCore.SignalR;
using AD_Coursework.Hubs;

namespace AD_Coursework.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly IHubContext<AnnouncementHub> _hubContext;

        public AnnouncementService(
            IAnnouncementRepository announcementRepository,
            IHubContext<AnnouncementHub> hubContext)
        {
            _announcementRepository = announcementRepository;
            _hubContext = hubContext;
        }

        public async Task<IEnumerable<AnnouncementDto>> GetAllAnnouncementsAsync()
        {
            var announcements = await _announcementRepository.GetAllAsync();

            var result = announcements.Select(a => new AnnouncementDto
            {
                Id = a.Id,
                Title = a.Title,
                Message = a.Message,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                CreatedAt = a.CreatedAt,
                IsActive = a.IsActive,
                Type = a.Type.ToString()
            });

            return result;
        }

        public async Task<AnnouncementDto?> GetAnnouncementByIdAsync(Guid id)
        {
            var announcement = await _announcementRepository.GetByIdAsync(id);

            if (announcement == null)
                return null;

            return new AnnouncementDto
            {
                Id = announcement.Id,
                Title = announcement.Title,
                Message = announcement.Message,
                StartDate = announcement.StartDate,
                EndDate = announcement.EndDate,
                CreatedAt = announcement.CreatedAt,
                IsActive = announcement.IsActive,
                Type = announcement.Type.ToString()
            };
        }

        public async Task<AnnouncementDto> CreateAnnouncementAsync(AnnouncementCreateDto createDto)
        {
            var announcement = new Announcement
            {
                Title = createDto.Title,
                Message = createDto.Message,
                StartDate = createDto.StartDate,
                EndDate = createDto.EndDate,
                CreatedAt = DateTime.UtcNow,  
                Type = createDto.Type
            };

            announcement = await _announcementRepository.AddAsync(announcement);

            return new AnnouncementDto
            {
                Id = announcement.Id,
                Title = announcement.Title,
                Message = announcement.Message,
                StartDate = announcement.StartDate,
                EndDate = announcement.EndDate,
                CreatedAt = announcement.CreatedAt,
                IsActive = announcement.IsActive,
                Type = announcement.Type.ToString()
            };
        }

        public async Task<AnnouncementDto?> UpdateAnnouncementAsync(Guid id, AnnouncementUpdateDto updateDto)
        {
            var existingAnnouncement = await _announcementRepository.GetByIdAsync(id);
            if (existingAnnouncement == null) return null;

            existingAnnouncement.Title = updateDto.Title;
            existingAnnouncement.Message = updateDto.Message;
            existingAnnouncement.StartDate = updateDto.StartDate;
            existingAnnouncement.EndDate = updateDto.EndDate;
            existingAnnouncement.IsActive = updateDto.IsActive;
            existingAnnouncement.Type = updateDto.Type;

            existingAnnouncement = await _announcementRepository.UpdateAsync(existingAnnouncement);

            return new AnnouncementDto
            {
                Id = existingAnnouncement.Id,
                Title = existingAnnouncement.Title,
                Message = existingAnnouncement.Message,
                StartDate = existingAnnouncement.StartDate,
                EndDate = existingAnnouncement.EndDate,
                CreatedAt = existingAnnouncement.CreatedAt,
                IsActive = existingAnnouncement.IsActive,
                Type = existingAnnouncement.Type.ToString()
            };
        }

        public async Task<bool> DeleteAnnouncementAsync(Guid id)
        {
            return await _announcementRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<AnnouncementDto>> GetActiveAnnouncementsAsync()
        {
            var announcements = await _announcementRepository.GetActiveAnnouncementsAsync();

            return announcements.Select(a => new AnnouncementDto
            {
                Id = a.Id,
                Title = a.Title,
                Message = a.Message,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                CreatedAt = a.CreatedAt,
                IsActive = a.IsActive,
                Type = a.Type.ToString()
            });
        }

        public async Task BroadcastAnnouncementAsync(Guid announcementId)
        {
            var announcement = await _announcementRepository.GetByIdAsync(announcementId);
            if (announcement == null || !announcement.IsActive) return;

            var now = DateTime.UtcNow;
            if (announcement.StartDate > now || announcement.EndDate < now) return;

            // Manual mapping to AnnouncementDto
            var announcementDto = new AnnouncementDto
            {
                Id = announcement.Id,
                Title = announcement.Title,
                Message = announcement.Message,
                StartDate = announcement.StartDate,
                EndDate = announcement.EndDate,
                CreatedAt = announcement.CreatedAt,
                IsActive = announcement.IsActive,
                Type = announcement.Type.ToString()
            };

            await _hubContext.Clients.All.SendAsync("ReceiveAnnouncement", announcementDto);
        }
    }
}