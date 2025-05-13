using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.DTOs.Announcement
{
    public class AnnouncementUpdateDto
    {
        [Required(ErrorMessage = "Announcement Title is required.")]
        [StringLength(200, ErrorMessage = "Announcement Title cannot exceed 200 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Announcement Content is required.")]
        [StringLength(1000, ErrorMessage = "Announcement Content cannot exceed 1000 characters.")]
        public string Message { get; set; } = string.Empty;

        [Required(ErrorMessage = "Start Date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Announcement Type is required.")]
        public string Type { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
