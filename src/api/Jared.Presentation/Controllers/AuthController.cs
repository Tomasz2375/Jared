using Jared.Contracts.Auth;
using Jared.Core.Abstractions;
using Jared.Dtos.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jared.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator)
{
    [HttpPost("register")]
    public async Task<Result<bool>> UserRegisterAsync(RegisterRequestDto dto)
    {
        return await mediator.Send(new RegisterCommand(dto));
    }

    [HttpPost("login")]
    public async Task<Result<LoginResponseDto>> UserLoginAsync(LoginRequestDto dto)
    {
        return await mediator.Send(new LoginCommand(dto));
    }

    [HttpPost("logout")]
    public async Task<Result<bool>> UserLogoutAsync(RefreshTokenDto dto)
    {
        return await mediator.Send(new LogoutCommand(dto));
    }

    [HttpPost("refresh")]
    public async Task<Result<string>> RefreshTokenAsync(RefreshTokenDto dto)
    {
        return await mediator.Send(new RefreshTokenCommand(dto));
    }
}
