using AD_Coursework.Models;
using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.DTOs.Order
{
    public class OrderUpdateDto
    {
        [Required]
        public OrderStatus Status { get; set; }
    }
}
