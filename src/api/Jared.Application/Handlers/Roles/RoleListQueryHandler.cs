using Jared.Contracts.Roles;
using Jared.Domain.Abstractions;
using Jared.Domain.Models;
using Jared.Dtos.Roles;
using Jared.Shared.Abstractions;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Handlers.Roles;

public class RoleListQueryHandler(IDataContext dataContext, IMapper mapper)
    : IRequestHandler<RoleListQuery, Result<List<RoleListDto>>>
{
    private readonly IDataContext dataContext = dataContext;
    private readonly IMapper mapper = mapper;

    public async Task<Result<List<RoleListDto>>> Handle(RoleListQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var rolesQuery = dataContext
                .Set<Role>()
                .AsNoTracking();

            var roles = await rolesQuery.ToListAsync();

            var result = mapper.Map<List<RoleListDto>>(roles);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<RoleListDto>>(ex.Message);
        }
    }
}
