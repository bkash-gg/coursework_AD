namespace AD_Coursework.DTOs.OrderItem
{
    public class OrderItemDto
    {
        public Guid OrderId { get; set; }
        public Guid BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }
}
