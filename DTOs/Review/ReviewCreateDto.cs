﻿using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.DTOs.Review
{
    public class ReviewCreateDto
    {
        [Required(ErrorMessage = "Rating is required")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }

        [StringLength(1000, ErrorMessage = "Comment cannot exceed 1000 characters")]
        public string? Comment { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Book ID is required")]
        public Guid BookId { get; set; }
    }
}
