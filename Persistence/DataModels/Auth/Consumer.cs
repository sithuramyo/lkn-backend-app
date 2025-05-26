namespace Persistence.DataModels.Auth;

public class Consumer : BaseDataModel
{
    public string ConsumerCode { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool EmailVerified { get; set; }
    public string Type { get; set; }
    public string? Provider { get; set; } // Google or Facebook
    public string? ProviderAccountId { get; set; } // Google or Facebook
    public string? Password { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? ProfilePicture { get; set; }
}