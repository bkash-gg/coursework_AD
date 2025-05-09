using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.DTOs.CartItem
{
    public class RemoveFromCartDto
    {
        [Required(ErrorMessage = "Book ID is required.")]
        public Guid BookId { get; set; }
    }
}
