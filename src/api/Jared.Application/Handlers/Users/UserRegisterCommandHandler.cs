using Jared.Contracts.Users;
using Jared.Core.Abstractions;
using Jared.Domain.Abstractions;
using Jared.Domain.Models;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Jared.Application.Handlers.Users;

public class UserRegisterCommandHandler(
    IDataContext dataContext,
    IMapper mapper,
    IPasswordHasher<User> passwordHasher)
    : IRequestHandler<UserRegisterCommand, Result<bool>>
{
    private readonly IDataContext dataContext = dataContext;
    private readonly IMapper mapper = mapper;
    private readonly IPasswordHasher<User> passwordHasher = passwordHasher;

    public async Task<Result<bool>> Handle(UserRegisterCommand command, CancellationToken cancellationToken)
    {
        try
        {
            if (dataContext.Set<User>().Any(x => x.Email == command.dto.Email))
            {
                return Result.Fail<bool>("A user with the given email address already exists in the database");
            }

            var user = mapper.Map<User>(command.dto);
            var hashedPassword = passwordHasher.HashPassword(user, command.dto.Password);
            user.PasswordHash = hashedPassword;
            user.RoleId = 1;

            dataContext.Add(user);

            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(true);
        }
        catch (Exception ex)
        {
            return Result.Fail<bool>(ex.Message);
        }
    }
}
