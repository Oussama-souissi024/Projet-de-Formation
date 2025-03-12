using AutoMapper;
using E_Commerce.Application.Auth.DTOs;
using E_Commerce.Application.Auth.Interfaces;
using E_Commerce.Core.Entities.Identity;
using E_Commerce.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace E_Commerce.Application.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ILogger<AuthService> _logger;
        private readonly IMapper _mapper;

        public AuthService(IAuthRepository authRepository,
                         ILogger<AuthService> logger,
                         IMapper mapper)
        {
            _authRepository = authRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            try
            {
                var user = new ApplicationUser
                {
                    UserName = registrationRequestDto.Email,
                    Email = registrationRequestDto.Email,
                    NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                    PhoneNumber = registrationRequestDto.PhoneNumber
                };

                var result = await _authRepository.CreateUserAsync(user, registrationRequestDto.Password);

                if (result.Succeeded)
                {
                    if (!await _authRepository.RoleExistsAsync(registrationRequestDto.Role))
                    {
                        await _authRepository.CreateRoleAsync(registrationRequestDto.Role);
                    }

                    await _authRepository.AddUserToRoleAsync(user, registrationRequestDto.Role);
                    return string.Empty;
                }

                return result.Errors.FirstOrDefault()?.Description ?? "Registration failed";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during registration for user: {Email}", registrationRequestDto.Email);
                return $"An error occurred: {ex.Message}";
            }
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _authRepository.GetUserByUsernameAsync(loginRequestDto.Email);

            if (user == null || !await _authRepository.CheckPasswordAsync(user, loginRequestDto.Password))
            {
                return null;
            }

            var roles = await _authRepository.GetUserRolesAsync(user);

            return new LoginResponseDto
            {
                ID = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Role = roles.FirstOrDefault() ?? string.Empty
            };
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = await _authRepository.GetUserByEmailAsync(email);

            if (user != null)
            {
                if (!await _authRepository.RoleExistsAsync(roleName))
                {
                    await _authRepository.CreateRoleAsync(roleName);
                }

                var result = await _authRepository.AddUserToRoleAsync(user, roleName);
                return result.Succeeded;
            }

            return false;
        }
    }
}
