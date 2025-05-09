using AD_Coursework.DTOs.OrderItem;
using AD_Coursework.Models;

namespace AD_Coursework.DTOs.Order
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal TotalAmount { get; set; }
        public bool UsedBulkDiscount { get; set; }
        public bool UsedLoyaltyDiscount { get; set; }
        public string ClaimCode { get; set; } = string.Empty;
        public string PickupNotes { get; set; } = string.Empty;
        public DateTime? PickupDate { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime? CancellationDate { get; set; }
        public string? CancellationReason { get; set; }
        public DateTime? CompletionDate { get; set; }
        public Guid UserId { get; set; }

        public string UserFullName { get; set; } = string.Empty;
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}
