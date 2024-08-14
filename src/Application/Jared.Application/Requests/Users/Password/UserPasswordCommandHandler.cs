using Jared.Application.Services.User;
using Jared.Domain.Abstractions;
using Jared.Shared.Interfaces;
using Jared.Domain.Models;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Requests.Users.Password;

public class UserPasswordCommandHandler : IRequestHandler<UserPasswordCommand, Result<bool>>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;
    private readonly IPasswordHasher<User> passwordHasher;
    private readonly IUserService userService;

    public UserPasswordCommandHandler(
        IDataContext dataContext,
        IMapper mapper,
        IPasswordHasher<User> passwordHasher,
        IUserService userService)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
        this.passwordHasher = passwordHasher;
        this.userService = userService;
    }

    public async Task<Result<bool>> Handle(UserPasswordCommand command, CancellationToken cancellationToken)
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

            if (command.dto.NewPassword != command.dto.ConfirmPassword)
            {
                return Result.Fail<bool>("The value of the new password and confirm password fields must be the same");
            }

            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                command.dto.CurrentPassword);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                return Result.Fail<bool>("The password you entered is incorrect");
            }

            user.PasswordHash = passwordHasher.HashPassword(user, command.dto.NewPassword);

            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(true);
        }
        catch (Exception ex)
        {
            return Result.Fail<bool>(ex.Message);
        }
    }
}
