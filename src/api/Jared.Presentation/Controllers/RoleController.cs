using Jared.Contracts.Roles;
using Jared.Core.Abstractions;
using Jared.Dtos.Roles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jared.Presentation.Controllers;

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
