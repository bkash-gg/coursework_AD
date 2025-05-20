namespace AD_Coursework.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; 
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
    }
}