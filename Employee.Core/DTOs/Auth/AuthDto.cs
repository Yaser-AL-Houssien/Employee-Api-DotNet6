using System.Text.Json.Serialization;

namespace Employee.Core.DTOs;
public class AuthDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresOn { get; set; }
    public DateTime RefreshTokenExpiration { get; set; }
    public string Message { get; set; }

    #region  Ignored props
    [JsonIgnore] public bool IsAuthenticated { get; set; } //by default = false
    [JsonIgnore] public string? RefreshToken { get; set; } //send it in cookie
    #endregion
}
