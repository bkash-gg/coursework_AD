using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.DTOs.Order
{
    public class OrderProcessDto
    {
        [Required]
        public string ClaimCode { get; set; }

        [Required]
        public Guid MemberId { get; set; }
    }
}
