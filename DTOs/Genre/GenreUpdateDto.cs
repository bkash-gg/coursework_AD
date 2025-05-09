using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.DTOs.Genre
{
    public class GenreUpdateDto
    {
        [Required(ErrorMessage = "Genre Name is required.")]
        [StringLength(50, ErrorMessage = "Genre Name cannot exceed 50 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
        public string Description { get; set; } = string.Empty;
    }
}
