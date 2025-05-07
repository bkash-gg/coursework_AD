using AD_Coursework.Models;

namespace AD_Coursework.DTOs.Order
{
    public class OrderListFilterDto
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public OrderStatus? Status { get; set; }
        public string? SearchTerm { get; set; }
        public string SortBy { get; set; } = "OrderDate";
        public bool SortDescending { get; set; } = true;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
