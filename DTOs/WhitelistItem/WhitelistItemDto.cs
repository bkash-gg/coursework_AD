namespace AD_Coursework.DTOs.WhitelistItem
{
    public class WhitelistItemDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public string BookTitle { get; set; }
        public string BookImageURL { get; set; }
        public string AuthorName { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
