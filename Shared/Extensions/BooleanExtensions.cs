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
            AdminRole.SuperAdmin => true,
            AdminRole.Admin => true,
            AdminRole.Retail => true,
            AdminRole.WholeSale => true,
            _ => false
        };
    }
}