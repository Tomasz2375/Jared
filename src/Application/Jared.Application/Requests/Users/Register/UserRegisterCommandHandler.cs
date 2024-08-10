using Jared.Domain.Abstractions;
using Jared.Domain.Interfaces;
using Jared.Domain.Models;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Jared.Application.Requests.Users.Register;

public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, Result<bool>>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;
    private readonly IPasswordHasher<User> passwordHasher;

    public UserRegisterCommandHandler(
        IDataContext dataContext,
        IMapper mapper,
        IPasswordHasher<User> passwordHasher
    )
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
        this.passwordHasher = passwordHasher;
    }
    public async Task<Result<bool>> Handle(UserRegisterCommand command, CancellationToken cancellationToken)
    {
        try
        {
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
