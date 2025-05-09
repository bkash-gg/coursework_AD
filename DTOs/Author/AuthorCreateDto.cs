using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.DTOs.Author
{
    public class AuthorCreateDto
    {
        [Required(ErrorMessage = "Author Name is required.")]
        [StringLength(50, ErrorMessage = "Author Name cannot exceed 50 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "Bio cannot exceed 200 characters.")]
        public string? Bio { get; set; }
    }
}
