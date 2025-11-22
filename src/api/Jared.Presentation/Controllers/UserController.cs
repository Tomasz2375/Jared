using Jared.Contracts.Users;
using Jared.Dtos.Users;
using Jared.Shared.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jared.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IMediator mediator)
{
    private readonly IMediator mediator = mediator;

    [HttpGet("List")]
    public async Task<Result<List<UserListDto>>> UserListAsync()
    {
        return await mediator.Send(new UserListQuery());
    }

    [HttpPost("Register")]
    public async Task<Result<bool>> UserRegisterAsync(UserRegisterDto dto)
    {
        return await mediator.Send(new UserRegisterCommand(dto));
    }

    [HttpPost("Login")]
    public async Task<Result<string>> UserLoginAsync(UserLoginDto dto)
    {
        return await mediator.Send(new UserLoginCommand(dto));
    }

    [HttpPut("Password")]
    public async Task<Result<bool>> UserPasswordAsync(UserPasswordDto dto)
    {
        return await mediator.Send(new UserPasswordCommand(dto));
    }

    [HttpPut("Update")]
    public async Task<Result<bool>> UserUpdateAsync(UserUpdateDto dto)
    {
        return await mediator.Send(new UserUpdateCommand(dto));
    }

    [Authorize]
    [HttpPut("UpdateRole")]
    public async Task<Result<bool>> UserRoleUpdateAsync(UserRoleUpdateDto dto)
    {
        return await mediator.Send(new UserRoleUpdateCommand(dto));
    }
}
