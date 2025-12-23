using Jared.Application.Abstractions;
using Jared.Contracts.Auth;
using Jared.Core.Abstractions;
using Jared.Domain.Abstractions;
using Jared.Domain.Models;
using Jared.Dtos.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Handlers.Auth;

public class LoginCommandHandler(
    IDataContext dataContext,
    IRefreshTokenService refreshTokenService,
    IPasswordHasher<User> passwordHasher)
    : IRequestHandler<LoginCommand, Result<LoginResponseDto>>
{
    public async Task<Result<LoginResponseDto>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var email = request.dto.Email.Trim();
            var user = await dataContext.Set<User>()
                .Include(x => x.Role)
                .SingleOrDefaultAsync(x => x.Email == email, cancellationToken);
            if (user is null)
            {
                return Result.Fail<LoginResponseDto>("Invalid user name or password");
            }

            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return Result.Fail<LoginResponseDto>("Invalid user name or password");
            }

            if (result == PasswordVerificationResult.SuccessRehashNeeded)
            {
                user.PasswordHash = passwordHasher.HashPassword(user, request.dto.Password);
            }

            var refreshToken = await refreshTokenService.CreateAsync(user, cancellationToken);

            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(new LoginResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}",
                Role = user.Role!.Name,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiresAtUtc = refreshToken.ExpiresAtUtc,
            });
        }
        catch (Exception ex)
        {
            return Result.Fail<LoginResponseDto>(ex.Message);
        }
    }
}
