using AD_Coursework.DTOs.User;

namespace AD_Coursework.DTOs.AuthResponse
{
    public class AuthResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
        public DateTime? Expiration { get; set; }
        public string Message { get; set; }
    }
}
