using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.DTOs.WhitelistItem
{
    public class WhitelistItemCreateDto
    {
        [Required(ErrorMessage = "User ID is required")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Book ID is required")]
        public Guid BookId { get; set; }
    }
}
