using Jared.Application.Abstractions;
using Jared.Contracts.Auth;
using Jared.Core.Abstractions;
using Jared.Domain.Abstractions;
using Jared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Handlers.Auth;

public class LogoutCommandHandler(
    IDataContext dataContext,
    IRefreshTokenService refreshTokenService)
    : IRequestHandler<LogoutCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await refreshTokenService.ValidateAsync(request.dto.RefreshToken, cancellationToken);
            if (user is null)
            {
                return Result.Ok(true);
            }

            var userRefreshTokens = await dataContext
                .Set<RefreshToken>()
                .Where(x => x.User!.Id == user.Id && x.RevokedAtUtc == null)
                .ToListAsync(cancellationToken);
            userRefreshTokens.ForEach(x => x.RevokedAtUtc = DateTime.UtcNow);

            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(true);
        }
        catch
        {
            return Result.Ok(true);
        }
    }
}
