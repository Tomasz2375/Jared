using Jared.Contracts.Roles;
using Jared.Core.Abstractions;
using Jared.Dtos.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jared.Presentation.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RolesController(IMediator mediator)
{
    [HttpGet]
    public async Task<Result<List<RoleListDto>>> GetAll()
    {
        return await mediator.Send(new RoleListQuery());
    }
}
