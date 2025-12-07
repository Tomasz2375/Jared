using Jared.Contracts.Users;
using Jared.Core.Abstractions;
using Jared.Domain.Abstractions;
using Jared.Domain.Models;
using Jared.Dtos.Users;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Handlers.Users;

public class UserDetailsQueryHandler(IDataContext dataContext, IMapper mapper)
    : IRequestHandler<UserDetailsQuery, Result<UserDetailsDto>>
{
    public async Task<Result<UserDetailsDto>> Handle(UserDetailsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var user = await dataContext.Set<User>()
                .AsNoTracking()
                .FirstAsync(x => x.Id == query.id);

            var result = mapper.Map<UserDetailsDto>(user);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<UserDetailsDto>(ex.Message);
        }
    }
}
