using Jared.Contracts.Users;
using Jared.Core.Abstractions;
using Jared.Dtos.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jared.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator)
{
    [HttpPost("register")]
    public async Task<Result<bool>> UserRegisterAsync(UserRegisterDto dto)
    {
        return await mediator.Send(new UserRegisterCommand(dto));
    }

    [HttpPost("login")]
    public async Task<Result<string>> UserLoginAsync(UserLoginDto dto)
    {
        return await mediator.Send(new UserLoginCommand(dto));
    }
}
