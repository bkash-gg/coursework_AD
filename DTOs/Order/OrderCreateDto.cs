using System.ComponentModel.DataAnnotations;
using AD_Coursework.DTOs.OrderItem;

namespace AD_Coursework.DTOs.Order
{
    public class OrderCreateDto
    {
        [Required(ErrorMessage = "User ID is required.")]
        public Guid UserId { get; set; }

        public DateTime? PickupDate { get; set; }

        [StringLength(200, ErrorMessage = "Pickup notes cannot exceed 200 characters.")]
        public string PickupNotes { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Payment method cannot exceed 50 characters.")]
        public string PaymentMethod { get; set; } = "In-Store";
    }
}
