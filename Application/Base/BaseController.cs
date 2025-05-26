using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Base;

[ApiController, Authorize]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    [NonAction]
    protected string GetCurrentUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("User ID not found in token.");

        return userId;
    }
}