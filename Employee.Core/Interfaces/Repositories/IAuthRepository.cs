using Employee.Core.DTOs;

namespace Employee.Core.Interfaces;
public interface IAuthRepository
{
    Task<AuthDto> RegisterAsync(RegisterDto dto);
    Task<AuthDto> GetTokenAsync(LoginDto dto);
    Task<AuthDto> RefreshTokenAsync(string token);
    Task<bool> RevokeTokenAsync(string token);
}
