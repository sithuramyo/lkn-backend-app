using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Modules.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using Shared.Models.Auth;

namespace Application.Modules.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService service) : ControllerBase
{
    [HttpPost("admin-login")]
    public async Task<IActionResult> AdminLogin(RequestModel<LoginRequestModel> request)
    {
        var result = await service.AdminLoginAsync(request.Request);
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }
    
    [HttpPost("consumer-login")]
    public async Task<IActionResult> ConsumerLogin(RequestModel<LoginRequestModel> request)
    {
        var result = await service.ConsumerLoginAsync(request.Request);
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }
    
    [HttpPost("consumer-social-login")]
    public async Task<IActionResult> ConsumerSocialLogin(RequestModel<SocialLoginRequestModel> request)
    {
        var result = await service.ConsumerSocialLoginAsync(request.Request);
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }
    
    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var jti = User.FindFirstValue(JwtRegisteredClaimNames.Jti);
        var result = await service.LogoutAsync(jti!);
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }
}