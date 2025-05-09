namespace AD_Coursework.DTOs.CartItem
{
    public class CartItemDto
    {
        public Guid CartId { get; set; }
        public Guid BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string BookCoverImageUrl { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }
}
