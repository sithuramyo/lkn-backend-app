namespace Shared.Models.Auth;

public class LoginResponseModel
{
    public string AccessToken { get; set; } = null!;
    public long ExpireAt { get; set; }
}