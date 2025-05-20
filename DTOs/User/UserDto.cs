namespace AD_Coursework.DTOs.User
{
    public class UserDto
    {
        public string? Username { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? FullName { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public int TotalOrdersCompleted { get; set; }
        public bool IsEligibleForLoyaltyDiscount { get; set; }
    }
}
