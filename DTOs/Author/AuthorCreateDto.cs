using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.DTOs.Author
{
    public class AuthorCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? Bio { get; set; }
    }
}
