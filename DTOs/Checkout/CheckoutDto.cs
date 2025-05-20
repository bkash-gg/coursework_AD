namespace AD_Coursework.DTOs.Checkout
{
    public class CheckoutSummaryDto
    {
        public List<CheckoutItemDto> Items { get; set; } = new List<CheckoutItemDto>();
        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal TotalAmount { get; set; }
        public bool EligibleForBulkDiscount { get; set; }
        public bool EligibleForLoyaltyDiscount { get; set; }
        public int LoyaltyOrderCount { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }

    public class CheckoutItemDto
    {
        public Guid BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}