using E_Commerce.Application.Auth.DTOs;
using E_Commerce.Core.Entities.Identity;

namespace E_Commerce.Application.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
