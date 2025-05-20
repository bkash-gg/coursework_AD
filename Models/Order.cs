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
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        
        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Range(0, double.MaxValue)]
        public decimal Subtotal { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal DiscountAmount { get; set; } = 0;
        
        [Range(0, 100)]
        public decimal DiscountPercentage { get; set; } = 0;
        
        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        public bool UsedBulkDiscount { get; set; } = false;

        public bool UsedLoyaltyDiscount { get; set; } = false;

        [Required]
        [StringLength(30)]
        public string ClaimCode { get; set; } = string.Empty;

        public DateTime? CancellationDate { get; set; }
        
        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}