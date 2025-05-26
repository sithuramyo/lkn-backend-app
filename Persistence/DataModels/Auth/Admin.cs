namespace Persistence.DataModels.Auth;

public class Admin : BaseDataModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}