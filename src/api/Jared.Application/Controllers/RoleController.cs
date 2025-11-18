using Jared.Application.Requests.Roles.List;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.Role;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jared.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController(IMediator mediator)
{
    private readonly IMediator mediator = mediator;

    [HttpGet("List")]
    public async Task<Result<List<RoleListDto>>> RoleListAsync()
    {
        return await mediator.Send(new RoleListQuery());
    }
}
