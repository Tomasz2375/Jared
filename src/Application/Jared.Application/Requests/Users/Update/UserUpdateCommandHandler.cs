using Jared.Application.Services.User;
using Jared.Domain.Models;
using Jared.Shared.Abstractions;
using Jared.Shared.Interfaces;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Requests.Users.Update;

public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand, Result<bool>>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;
    private readonly IUserService userService;

    public UserUpdateCommandHandler(
        IDataContext dataContext,
        IMapper mapper,
        IUserService userService)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
        this.userService = userService;
    }

    public async Task<Result<bool>> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await dataContext
                .Set<User>()
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == userService.GetUser().Id, cancellationToken);

            if (user is null)
            {
                return Result.Fail<bool>("User not found");
            }
            user = request.dto.Adapt(user);

            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(true);
        }
        catch (Exception ex)
        {
            return Result.Fail<bool>(ex.Message);
        }
    }
}
