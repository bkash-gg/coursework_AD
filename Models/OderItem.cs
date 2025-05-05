using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.Models
{
    public class OrderItem
    {
        [Required]
        [ForeignKey("Order")]
        public Guid OrderId { get; set; }
        public required virtual Order Order { get; set; }

        [Required]
        [ForeignKey("Book")]
        public Guid BookId { get; set; }
        public required virtual Book Book { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal LineTotal { get; set; }
    }
}
