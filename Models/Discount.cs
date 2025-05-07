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
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [ForeignKey("Book")]
        public Guid BookId { get; set; }
        public virtual Book Book { get; set; } = null!;
    }
}
