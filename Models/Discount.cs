using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.Models
{
    public class Discount
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Range(0, 100)]
        public float DiscountPercentage { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        [Required]
        [ForeignKey("Book")]
        public Guid BookId { get; set; }
        public required virtual Book Book { get; set; } 
    }
}
