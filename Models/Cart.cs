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
        public required virtual User User { get; set; }

        public virtual ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
