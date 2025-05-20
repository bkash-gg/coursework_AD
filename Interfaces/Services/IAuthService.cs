using AD_Coursework.DTOs.Auth;
using AD_Coursework.DTOs.User;

namespace AD_Coursework.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(UserRegistrationDto userRegistrationDto);
        Task<UserLoginResponseDto> LoginAsync(string email, string password);
        Task<AuthResponseDto> ChangePasswordAsync(Guid userId, UserPasswordChangeDto passwordChangeDto);
        Task<AuthResponseDto> UpdateProfileAsync(Guid userId, UserProfileUpdateDto profileUpdateDto);
        Task<AuthResponseDto> RefreshTokenAsync(string token, string refreshToken);
        Task RevokeTokenAsync(Guid token);
        Task<ServiceResponse<UserDto>> GetProfileAsync(Guid userId);
    }
}
