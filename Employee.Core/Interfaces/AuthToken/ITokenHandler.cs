using Employee.Core.Entities.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Employee.Core.Interfaces;
public interface ITokenHandler
{
    Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user);
    Task<RefreshToken> CreateRefreshToken(ApplicationUser user);
}
