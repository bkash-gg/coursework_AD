using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.DTOs.Book
{
    public class BookInventoryUpdateDto
    {
        public Guid BookId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be 0 or greater")]
        public int StockQuantity { get; set; }

        public bool IsAvailable { get; set; }
    }
}
