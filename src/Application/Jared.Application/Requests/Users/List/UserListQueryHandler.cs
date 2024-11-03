using Jared.Domain.Models;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.UserDtos;
using Jared.Shared.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Requests.Users.List;

public class UserListQueryHandler : IRequestHandler<UserListQuery, Result<List<UserListDto>>>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public UserListQueryHandler(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    public async Task<Result<List<UserListDto>>> Handle(UserListQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var usersQuery = dataContext
                .Set<User>()
                .Include(x => x.Role)
                .AsNoTracking();

            var users = await usersQuery.ToListAsync();

            var result = mapper.Map<List<UserListDto>>(users);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<UserListDto>>(ex.Message);
        }
    }
}
