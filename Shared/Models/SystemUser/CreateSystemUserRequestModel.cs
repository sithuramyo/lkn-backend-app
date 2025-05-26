using Shared.Attributes;

namespace Shared.Models.SystemUser;

public class CreateSystemUserRequestModel
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = null!;
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character")]
    public string Password { get; set; } = null!;
    [Required(ErrorMessage = "Role is required")]
    [ValidateRole]
    public string Role { get; set; } = null!;
}

public class UpdateSystemUserRequestModel
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = null!;

    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character")]
    public string? Password { get; set; }  // nullable to allow skipping

    [Required(ErrorMessage = "Role is required")]
    [ValidateRole]
    public string Role { get; set; } = null!;
}