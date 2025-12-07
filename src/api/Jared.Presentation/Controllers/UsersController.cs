using Jared.Contracts.Users;
using Jared.Core.Abstractions;
using Jared.Dtos.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jared.Presentation.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController(IMediator mediator)
{
    [HttpGet("{id}")]
    public async Task<Result<UserDetailsDto>> GetById([FromRoute] int id)
    {
        return await mediator.Send(new UserDetailsQuery(id));
    }

    [HttpGet]
    public async Task<Result<List<UserListDto>>> GetAll()
    {
        return await mediator.Send(new UserListQuery());
    }

    [HttpPut("{id}")]
    public async Task<Result<bool>> Update([FromRoute] int id, [FromBody] UserDetailsDto dto)
    {
        return await mediator.Send(new UserUpdateCommand(dto));
    }

    [HttpPatch("{id}/password")]
    public async Task<Result<bool>> UpdatePassword([FromRoute] int id, [FromBody] UserPasswordDto dto)
    {
        return await mediator.Send(new UserPasswordCommand(dto));
    }

    [HttpPatch("{id}/role")]
    public async Task<Result<bool>> UpdateRole([FromRoute] int id, [FromBody] UserRoleUpdateDto dto)
    {
        return await mediator.Send(new UserRoleUpdateCommand(dto));
    }
}
