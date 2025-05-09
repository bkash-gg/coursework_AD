using AD_Coursework.Models;
using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.DTOs.Order
{
    public class OrderUpdateDto
    {
        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; } = string.Empty; 

        public DateTime? PickupDate { get; set; }

        [StringLength(200, ErrorMessage = "Pickup notes cannot exceed 200 characters.")]
        public string PickupNotes { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Payment method cannot exceed 50 characters.")]
        public string PaymentMethod { get; set; } = string.Empty;

        public string? CancellationReason { get; set; }
    }
}
