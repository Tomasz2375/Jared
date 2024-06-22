using Jared.Application.Commands.UserCommands;
using Jared.Application.Dtos.UserDtos;
using Jared.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jared.Presentation.Controllers;

[ApiController]
[Route("controller")]
public class UserController
{
    private readonly IMediator mediator;

    public UserController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<Result> UserRegister(UserRegisterDto dto)
    {
        return await mediator.Send(new UserRegisterCommand(dto));
    }

    [HttpPost("login")]
    public async Task<Result<string>> UserLogin(UserLoginDto dto)
    {
        return await mediator.Send(new UserLoginCommand(dto));
    }
}
