namespace AD_Coursework.DTOs.Discount
{
    public class DiscountDto
    {
        public Guid Id { get; set; }
        public float DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid BookId { get; set; }

        public string BookTitle { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
    }
}
