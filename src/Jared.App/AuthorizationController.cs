using System.Security.Claims;
using Jared.Contracts.Auth;
using Jared.Core.Abstractions;
using Jared.Dtos.Auth;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jared.App;

[ApiController]
[Route("authorization")]
public class AuthorizationController(IMediator mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        var result = await mediator.Send(new LoginCommand(dto));
        if (result?.Success != true || result.Data is null)
        {
            return Unauthorized();
        }

        var user = result.Data;
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.FullName),
            new(ClaimTypes.Role, user.Role),
            new(ClaimTypes.Email, user.Email),
        };

        Response.Cookies.Append("refresh_token", user.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Expires = user.RefreshTokenExpiresAtUtc,
        });

        var principal = new ClaimsPrincipal(
            new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = user.RefreshTokenExpiresAtUtc,
            AllowRefresh = true,
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            authProperties);

        return Ok();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var refreshToken = HttpContext.Request.Cookies["refresh_token"];
        RefreshTokenDto dto = new()
        {
            RefreshToken = refreshToken ?? string.Empty,
        };

        var result = await mediator.Send(new LogoutCommand(dto));
        if (!result.Success)
        {
            return Unauthorized();
        }

        Response.Cookies.Delete("refresh_token");
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Ok();
    }
}
