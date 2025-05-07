namespace AD_Coursework.DTOs.User
{
    public class UserDetailDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int OrderCount { get; set; }
    }
}
