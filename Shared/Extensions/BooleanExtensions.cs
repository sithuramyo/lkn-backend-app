using Shared.Enums;

namespace Shared.Extensions;

public static class BooleanExtensions
{
    public static bool VerifyRole(this string role)
    {
        if (!Enum.TryParse(role, out AdminRole parsedRole))
        {
            return false;
        }
        return parsedRole switch
        {
            AdminRole.SUPERADMIN => true,
            AdminRole.ADMIN => true,
            AdminRole.RETAIL => true,
            AdminRole.WHOLESALE => true,
            _ => false
        };
    }
}