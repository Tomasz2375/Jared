using System.Security.Claims;
using Jared.Contracts.Auth;
using Jared.Dtos.Auth;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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

        var cookieName = HttpContext
            .RequestServices
            .GetRequiredService<IWebHostEnvironment>()
            .IsDevelopment()
            ? "refresh_token_dev"
            : "refresh_token";
        Response.Cookies.Append(cookieName, user.RefreshToken, new CookieOptions
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
        var cookieName = HttpContext
            .RequestServices
            .GetRequiredService<IWebHostEnvironment>()
            .IsDevelopment()
            ? "refresh_token_dev"
            : "refresh_token";
        var refreshToken = HttpContext.Request.Cookies[cookieName];
        RefreshTokenDto dto = new()
        {
            RefreshToken = refreshToken ?? string.Empty,
        };

        var result = await mediator.Send(new LogoutCommand(dto));
        if (!result.Success)
        {
            return Unauthorized();
        }

        Response.Cookies.Delete(cookieName);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Ok();
    }
}
