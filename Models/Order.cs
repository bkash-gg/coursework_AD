using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.Models
{
    public enum OrderStatus
    {
        Pending,
        Cancelled,
        Completed
    }

    public class Order
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(20)]
        public required string ClaimCode { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Subtotal { get; set; }

        [Range(0, double.MaxValue)]
        public decimal DiscountAmount { get; set; } = 0;

        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public required virtual User User { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
