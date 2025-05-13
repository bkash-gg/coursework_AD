using AD_Coursework.DTOs.Announcement;
using AD_Coursework.Extensions;
using AD_Coursework.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AD_Coursework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : BaseController
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementController(
            IAnnouncementService announcementService,
            ILogger<AnnouncementController> logger) : base(logger)
        {
            _announcementService = announcementService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAnnouncements()
        {
            try
            {
                var announcements = await _announcementService.GetAllAnnouncementsAsync();
                return Success(announcements);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnnouncementById(Guid id)
        {
            try
            {
                var announcement = await _announcementService.GetAnnouncementByIdAsync(id);
                if (announcement == null)
                {
                    return Error("Announcement not found", StatusCodes.Status404NotFound);
                }

                return Success(announcement);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateAnnouncement([FromBody] AnnouncementCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Error("Invalid data", StatusCodes.Status400BadRequest, ModelState);
                }

                var announcement = await _announcementService.CreateAnnouncementAsync(createDto);
                return Success(announcement, "Announcement created successfully", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnnouncement(Guid id, [FromBody] AnnouncementUpdateDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Error("Invalid data", StatusCodes.Status400BadRequest, ModelState);
                }

                var announcement = await _announcementService.UpdateAnnouncementAsync(id, updateDto);
                if (announcement == null)
                {
                    return Error("Announcement not found", StatusCodes.Status404NotFound);
                }

                return Success(announcement, "Announcement updated successfully");
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(Guid id)
        {
            try
            {
                var result = await _announcementService.DeleteAnnouncementAsync(id);
                if (!result)
                {
                    return Error("Announcement not found", StatusCodes.Status404NotFound);
                }

                return Success<object>(null, "Announcement deleted successfully");
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveAnnouncements()
        {
            try
            {
                var announcements = await _announcementService.GetActiveAnnouncementsAsync();
                return Success(announcements);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/broadcast")]
        public async Task<IActionResult> BroadcastAnnouncement(Guid id)
        {
            try
            {
                await _announcementService.BroadcastAnnouncementAsync(id);
                return Success<object>(null, "Announcement broadcasted successfully");
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}