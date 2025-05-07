using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.DTOs.Announcement
{
    public class AnnouncementUpdateDto
    {
        [StringLength(200, ErrorMessage = "Announcement Title cannot exceed 200 characters.")]
        public string? Title { get; set; }

        [StringLength(1000, ErrorMessage = "Announcement Content cannot exceed 1000 characters.")]
        public string? Content { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(50, ErrorMessage = "Announcement Type cannot exceed 50 characters.")]
        public string? Type { get; set; }
    }
}
