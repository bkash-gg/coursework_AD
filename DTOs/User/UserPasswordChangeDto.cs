using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.DTOs.User
{
    public class UserPasswordChangeDto
    {
        [Required(ErrorMessage = "Current password is required.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "New password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "New password must be at least 6 characters.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please confirm the new password.")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
