using AD_Coursework.Models;

namespace AD_Coursework.DTOs.Order
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string ClaimCode { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public string StatusDisplay => Status.ToString();
        public Guid UserId { get; set; }
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
    }
}
