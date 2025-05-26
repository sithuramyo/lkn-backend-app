using System.ComponentModel.DataAnnotations;

namespace Shared.Models.Admin;

public class CreateAdminRequestModel
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Role { get; set; }
}

public class AdminListResponseModel
{
    public List<AdminModel> Admins { get; set; } = [];
}

public class AdminResponseModel
{
    public AdminModel Admin { get; set; } = new();
}

public class AdminModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}