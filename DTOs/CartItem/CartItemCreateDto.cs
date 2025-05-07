using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.DTOs.CartItem
{
    public class CartItemCreateDto
    {
        [Required]
        public Guid BookId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
    }
}
