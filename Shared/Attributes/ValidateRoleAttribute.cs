using Shared.Enums;

namespace Shared.Attributes;

public class ValidateRoleAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string roleStr || string.IsNullOrWhiteSpace(roleStr))
        {
            return new ValidationResult("Role is required.");
        }

        var isValid = Enum.TryParse<AdminRole>(roleStr, true, out var role);
        if (!isValid)
        {
            return new ValidationResult($"'{roleStr}' is not a valid role.");
        }

        return role == AdminRole.SuperAdmin
            ? new ValidationResult("You are not allowed to assign the SuperAdmin role.")
            : ValidationResult.Success;
    }
}