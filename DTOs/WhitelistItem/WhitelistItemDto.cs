namespace AD_Coursework.DTOs.WhitelistItem
{
    public class WhitelistItemDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
