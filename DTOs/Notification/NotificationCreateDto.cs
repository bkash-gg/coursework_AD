using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.DTOs.Notification
{
    public class NotificationCreateDto
    {
        [Required(ErrorMessage = "Notification Message is required.")]
        [StringLength(500, ErrorMessage = "Notification Message cannot exceed 500 characters.")]
        public string Message { get; set; } = string.Empty;

        [Required(ErrorMessage = "User ID is required.")]
        public Guid UserId { get; set; }
    }
}
