namespace AD_Coursework.DTOs.Review
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public String? FullName { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
    }
}
