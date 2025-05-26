namespace Shared.Models.Auth;

public class SocialLoginRequestModel
{
    public string Email { get; set; } // Email from Google/Facebook
    public string Provider { get; set; } // "Google" or "Facebook"
    public string Type { get; set; }
    public bool EmailVerified { get; set; }
    public string ProviderAccountId { get; set; } // Unique ID from OAuth Provider
    public string? Name { get; set; } // Optional (If not available, fetch from Google/Facebook)
    public string? ProfilePicture { get; set; } // Optional
}