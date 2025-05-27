using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Models.Auth;

namespace Shared.Helpers;

public class TokenHelper(IConfiguration config)
{
    public LoginResponseModel GenerateToken(string id, string username, string? role = null)
    {
        var issuer = config["Jwt:Issuer"];
        var audience = config["Jwt:Audience"];
        var secretKey = config["Jwt:Secret"];

        if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience) || string.IsNullOrEmpty(secretKey))
            throw new InvalidOperationException("JWT configuration is missing required values.");

        var expireDays = Convert.ToInt32(config["Jwt:ExpireDays"]);
        var key = Encoding.UTF8.GetBytes(secretKey);
        var securityKey = new SymmetricSecurityKey(key);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var now = DateTime.UtcNow;
        var expiresAt = now.AddDays(expireDays);
        var iatUnix = new DateTimeOffset(now).ToUnixTimeSeconds();
        var expUnix = new DateTimeOffset(expiresAt).ToUnixTimeSeconds();

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, id),
            new(ClaimTypes.NameIdentifier, id),
            new(JwtRegisteredClaimNames.Name, username),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, iatUnix.ToString(), ClaimValueTypes.Integer64),
            new(JwtRegisteredClaimNames.Nbf, iatUnix.ToString(), ClaimValueTypes.Integer64),
            new(JwtRegisteredClaimNames.Exp, expUnix.ToString(), ClaimValueTypes.Integer64),
            new(JwtRegisteredClaimNames.Iss, issuer),
            new(JwtRegisteredClaimNames.Aud, audience)
        };

        if (!string.IsNullOrEmpty(role))
            claims.Add(new Claim(JwtRegisteredClaimNames.Typ, role));

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            notBefore: now,
            expires: expiresAt,
            signingCredentials: credentials
        );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new LoginResponseModel
        {
            AccessToken = accessToken,
            ExpireAt = expUnix
        };
    }
}