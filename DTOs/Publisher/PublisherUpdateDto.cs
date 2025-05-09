using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.DTOs.Publisher
{
    public class PublisherUpdateDto
    {
        [Required(ErrorMessage = "Publisher Name is required.")]
        [StringLength(50, ErrorMessage = "Publisher Name cannot exceed 50 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(100, ErrorMessage = "Address cannot exceed 100 characters.")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone Number is required.")]
        [StringLength(15, ErrorMessage = "Phone Number cannot exceed 15 characters.")]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
