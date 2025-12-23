using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Jared.Application.Abstractions;
using Jared.Domain.Models;
using Jared.Domain.Options;
using Microsoft.IdentityModel.Tokens;

namespace Jared.Application.Services;

public class JwtTokenService(AuthenticationOptions authenticationOptions) : IJwtTokenService
{
    public string GenerateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new(ClaimTypes.Role, user.Role!.Name),
        };
        var expiresAt = DateTime.UtcNow.AddMinutes(authenticationOptions.JwtExpireMinutes);
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(authenticationOptions.JwtKey));
        var token = new JwtSecurityToken(
            issuer: authenticationOptions.JwtIssurer,
            audience: authenticationOptions.JwtIssurer,
            claims: claims,
            expires: expiresAt,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}