namespace AD_Coursework.DTOs.Book
{
    public class BookFilterDto
    {
        public string SearchTerm { get; set; } = string.Empty;
        public List<Guid> AuthorIds { get; set; } = new List<Guid>();
        public List<Guid> GenreIds { get; set; } = new List<Guid>();
        public List<Guid> PublisherIds { get; set; } = new List<Guid>();
        public bool? IsAvailable { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public double? MinRating { get; set; }
        public List<string> Languages { get; set; } = new List<string>();
        public List<string> Formats { get; set; } = new List<string>();
        public bool? IsOnSale { get; set; }
        public bool? IsAwardWinner { get; set; }
        public bool? IsNewRelease { get; set; }
        public bool? IsNewArrival { get; set; }
        public bool? IsComingSoon { get; set; }
        public string SortBy { get; set; } = "Title"; 
        public bool SortDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
