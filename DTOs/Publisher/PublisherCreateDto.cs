using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.DTOs.Publisher
{
    public class PublisherCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(500)]
        public string Address { get; set; }

        [Required]
        [StringLength(15)]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
