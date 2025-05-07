using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.DTOs.Review
{
    public class ReviewCreateDto
    {
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(1000)]
        public string Comment { get; set; }

        [Required]
        public Guid BookId { get; set; }
    }
}
