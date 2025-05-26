namespace Shared.Models.Consumers;

public class ConsumerListResponseModel
{
    public List<ConsumerModel> Consumers { get; set; } = [];
}

public class ConsumerModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Provider { get; set; }
    public string ProviderAccountId { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string ProfilePicture { get; set; }
    public bool EmailVerified { get; set; }
    public string Type { get; set; }
}