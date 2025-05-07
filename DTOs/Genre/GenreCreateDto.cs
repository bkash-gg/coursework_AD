using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.DTOs.Genre
{
    public class GenreCreateDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }
    }
}
