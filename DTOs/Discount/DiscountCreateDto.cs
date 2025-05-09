using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.DTOs.Discount
{
    public class DiscountCreateDto
    {
        [Required(ErrorMessage = "Discount percentage is required.")]
        [Range(0, 100, ErrorMessage = "Discount percentage must be between 0 and 100.")]
        public float DiscountPercentage { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Book ID is required.")]
        public Guid BookId { get; set; }
    }
}
