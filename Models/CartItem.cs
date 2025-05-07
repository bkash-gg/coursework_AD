using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.Models
{
    public class CartItem
    {
        [Required]
        [ForeignKey("Cart")]
        public Guid CartId { get; set; }
        public required virtual Cart Cart { get; set; }

        [Required]
        [ForeignKey("Book")]
        public Guid BookId { get; set; }
        public required virtual Book Book { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }
    }
}
