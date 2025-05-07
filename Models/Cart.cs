using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.Models
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;

        [Required]
        public int ItemCount { get; set; } = 0;

        [Range(0, double.MaxValue)]
        public decimal Subtotal { get; set; } = 0;

        public virtual ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
