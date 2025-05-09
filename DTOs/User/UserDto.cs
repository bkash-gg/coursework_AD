namespace AD_Coursework.DTOs.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
        public string? Address { get; set; }
        public int TotalOrdersCompleted { get; set; }
        public bool IsEligibleForLoyaltyDiscount { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }
}
