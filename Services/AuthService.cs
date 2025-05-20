using AD_Coursework.Interfaces.Repositories;
using AD_Coursework.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using AD_Coursework.Extensions;
using AD_Coursework.DTOs.Auth;
using AD_Coursework.DTOs.User;
using AD_Coursework.Models;
using AD_Coursework.Utils;

namespace AD_Coursework.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtHandler _jwtHandler;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AuthService(
            IUserRepository userRepository,
            JwtHandler jwtHandler,
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<AuthResponseDto> RegisterAsync(UserRegistrationDto userRegistrationDto)
        {
            if (await _userRepository.UserExistsAsync(userRegistrationDto.Username))
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Username already taken.",
                };
            }

            if (await _userRepository.EmailExistsAsync(userRegistrationDto.Email))
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Email already in use.",
                };
            }

            var passwordValidator = new PasswordValidator<User>();
            var passwordValidation = await passwordValidator.ValidateAsync(_userManager, null!, userRegistrationDto.Password);
            if (!passwordValidation.Succeeded)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Password too weak. Try a stronger one."
                };
            }

            var memberRole = await _roleManager.FindByNameAsync("Member");
            if (memberRole == null)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "System error. Please try again later."
                };
            }

            var newUser = new User
            {
                UserName = userRegistrationDto.Username,
                NormalizedUserName = userRegistrationDto.Username.ToUpper(),
                Email = userRegistrationDto.Email,
                NormalizedEmail = userRegistrationDto.Email.ToUpper(),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                RoleId = memberRole.Id
            };

            var result = await _userRepository.CreateUserAsync(newUser, userRegistrationDto.Password);
            if (result == null)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Failed to create account. Please try again."
                };
            }

            return new AuthResponseDto
            {
                IsSuccess = true,
                Message = "Registration successful! Please log in."
            };
        }

        public async Task<UserLoginResponseDto> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                return new UserLoginResponseDto
                {
                    AuthResponse = new AuthResponseDto
                    {
                        IsSuccess = false,
                        Message = "Invalid email or password. Please try again."
                    }
                };
            }
            var role = await _userRepository.GetUserRoleNameAsync(user.Id);

            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = _jwtHandler.GetClaims(user, role);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            var refreshToken = _jwtHandler.GenerateRefreshToken();
            var refreshTokenExpiry = DateTime.UtcNow.AddDays(Convert.ToDouble(_jwtHandler.JwtSettings["RefreshTokenExpiryInDays"]));

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = refreshTokenExpiry;
            await _userManager.UpdateAsync(user);

            var userDto = new UserDto
            {
                Username = user.UserName,
                Email = user.Email,
                FullName = user.FullName,
                RegistrationDate = user.RegistrationDate,
                Address = user.Address,
                TotalOrdersCompleted = user.TotalOrdersCompleted,
                IsEligibleForLoyaltyDiscount = user.IsEligibleForLoyaltyDiscount
            };


            return new UserLoginResponseDto
            {
                AuthResponse = new AuthResponseDto
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    UserId = user.Id.ToString(), 
                    Role = role,
                    Email = user.Email,
                    IsSuccess = true,
                    Message = "Login successful."
                }
            };
        }

        public async Task<AuthResponseDto> ChangePasswordAsync(Guid userId, UserPasswordChangeDto passwordChangeDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(passwordChangeDto.CurrentPassword))
                {
                    return new AuthResponseDto
                    {
                        IsSuccess = false,
                        Message = "Current password is required."
                    };
                }

                if (string.IsNullOrWhiteSpace(passwordChangeDto.NewPassword))
                {
                    return new AuthResponseDto
                    {
                        IsSuccess = false,
                        Message = "New password is required."
                    };
                }

                if (string.IsNullOrWhiteSpace(passwordChangeDto.ConfirmNewPassword))
                {
                    return new AuthResponseDto
                    {
                        IsSuccess = false,
                        Message = "Please confirm your new password."
                    };
                }

                if (passwordChangeDto.NewPassword != passwordChangeDto.ConfirmNewPassword)
                {
                    return new AuthResponseDto
                    {
                        IsSuccess = false,
                        Message = "New password and confirmation password do not match."
                    };
                }

                var user = await _userRepository.GetUserByIdAsync(userId);
                if (user == null)
                {
                    return new AuthResponseDto
                    {
                        IsSuccess = false,
                        Message = "Account not found. Please try logging in again."
                    };
                }

                var isPasswordValid = await _userManager.CheckPasswordAsync(user, passwordChangeDto.CurrentPassword);
                if (!isPasswordValid)
                {
                    return new AuthResponseDto
                    {
                        IsSuccess = false,
                        Message = "The current password you entered is incorrect. Please try again."
                    };
                }

                var result = await _userManager.ChangePasswordAsync(
                    user,
                    passwordChangeDto.CurrentPassword,
                    passwordChangeDto.NewPassword);

                if (!result.Succeeded)
                {
                    return new AuthResponseDto
                    {
                        IsSuccess = false,
                        Message = "We couldn't update your password."
                    };
                }

                return new AuthResponseDto
                {
                    IsSuccess = true,
                    Message = "Your password has been successfully updated!"
                };
            }
            catch (Exception ex)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "We encountered an issue while updating your password."
                };
            }
        }

        public async Task<AuthResponseDto> UpdateProfileAsync(Guid userId, UserProfileUpdateDto profileUpdateDto)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(userId);
                if (user == null)
                {
                    return new AuthResponseDto
                    {
                        IsSuccess = false,
                        Message = "User not found"
                    };
                }

                if (user.Email != profileUpdateDto.Email)
                {
                    var emailExists = await _userManager.FindByEmailAsync(profileUpdateDto.Email);
                    if (emailExists != null)
                    {
                        return new AuthResponseDto
                        {
                            IsSuccess = false,
                            Message = "Email is already in use by another account"
                        };
                    }
                }

                if (user.UserName != profileUpdateDto.Username)
                {
                    var usernameExists = await _userManager.FindByNameAsync(profileUpdateDto.Username);
                    if (usernameExists != null)
                    {
                        return new AuthResponseDto
                        {
                            IsSuccess = false,
                            Message = "Username is already taken"
                        };
                    }
                }

                user.UserName = profileUpdateDto.Username;
                user.NormalizedUserName = profileUpdateDto.Username.ToUpper();
                user.Email = profileUpdateDto.Email;
                user.NormalizedEmail = profileUpdateDto.Email.ToUpper();
                user.FullName = profileUpdateDto.FullName;
                user.PhoneNumber = profileUpdateDto.PhoneNumber;
                user.Address = profileUpdateDto.Address;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return new AuthResponseDto
                    {
                        IsSuccess = false,
                        Message = "Profile update failed",
                    };
                }
                var role = await _userRepository.GetUserRoleNameAsync(user.Id);
                return new AuthResponseDto
                {
                    IsSuccess = true,
                    Message = "Profile updated successfully!",
                    UserId = user.Id.ToString(),
                    Role = role
                };
            }
            catch (Exception ex)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "An error occurred while updating profile"
                };
            }
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(string token, string refreshToken)
        {
            var principal = _jwtHandler.GetPrincipalFromExpiredToken(token);
            var userId = principal.GetUserId();

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiry <= DateTime.UtcNow)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid token or refresh token."
                };
            }
            var role = await _userRepository.GetUserRoleNameAsync(user.Id);

            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = _jwtHandler.GetClaims(user, role);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var newToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            var newRefreshToken = _jwtHandler.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new AuthResponseDto
            {
                Token = newToken,
                RefreshToken = newRefreshToken,
                IsSuccess = true,
                Message = "Token refreshed successfully."
            };
        }

        public async Task RevokeTokenAsync(Guid token)
        {
            var user = await _userRepository.GetUserByIdAsync(token);
            if (user != null)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task<ServiceResponse<UserDto>> GetProfileAsync(Guid userId)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(userId);

                if (user == null)
                {
                    return ServiceResponse<UserDto>.Failure("User not found");
                }
                int completedOrdersCount = user.Orders.Count(o => o.Status == OrderStatus.Completed);
                var userDto = new UserDto
                {
                    Username = user.UserName,
                    Email = user.Email,
                    FullName = user.FullName,
                    RegistrationDate = user.RegistrationDate,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    TotalOrdersCompleted = completedOrdersCount
                };
               
                return ServiceResponse<UserDto>.Success(userDto);
            }
            catch (Exception ex)
            {
                return ServiceResponse<UserDto>.Failure("Error retrieving profile");
            }
        }
    }
}
