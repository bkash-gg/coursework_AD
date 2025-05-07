namespace AD_Coursework.DTOs.CartItem
{
    public class CartItemDto
    {
        public Guid CartId { get; set; }
        public Guid BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal { get; set; }
        public string CoverImageUrl { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public int AvailableStock { get; set; }
    }
}
