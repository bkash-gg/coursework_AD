namespace AD_Coursework.DTOs.WhitelistItem
{
    public class WhitelistItemDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public string BookTitle { get; set; }
        public string AuthorName { get; set; }
        public decimal Price { get; set; }
        public string CoverImageUrl { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsOnSale { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
