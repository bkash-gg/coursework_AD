using AD_Coursework.DTOs.CartItem;

namespace AD_Coursework.DTOs.Cart
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int ItemCount { get; set; }
        public decimal Subtotal { get; set; }
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
    }
}
