using Microsoft.AspNetCore.Identity;

namespace Employee.Core.Entities.Models;
public class ApplicationUser : IdentityUser
{
    [MaxLength(10)]
    public string? FirstName { get; set; }

    [MaxLength(10)]
    public string? LastName { get; set; }

    public List<RefreshToken>? RefreshTokens { get; set; }
}
