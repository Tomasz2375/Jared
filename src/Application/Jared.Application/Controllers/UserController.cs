using Jared.Application.Dtos.UserDtos;
using Jared.Application.Requests.Users.Login;
using Jared.Application.Requests.Users.Password;
using Jared.Application.Requests.Users.Register;
using Jared.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jared.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController
{
    private readonly IMediator mediator;

    public UserController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("Register")]
    public async Task<Result> UserRegister(UserRegisterDto dto)
    {
        return await mediator.Send(new UserRegisterCommand(dto));
    }

    [HttpPost("Login")]
    public async Task<Result<string>> UserLogin(UserLoginDto dto)
    {
        return await mediator.Send(new UserLoginCommand(dto));
    }

    [HttpPut("Password")]
    public async Task<Result<bool>> UserPassword(UserPasswordDto dto)
    {
        return await mediator.Send(new UserPasswordCommand(dto));
    }
}
