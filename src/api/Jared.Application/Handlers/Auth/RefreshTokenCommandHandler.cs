using System.Security;
using Jared.Application.Abstractions;
using Jared.Contracts.Auth;
using Jared.Core.Abstractions;
using MediatR;

namespace Jared.Application.Handlers.Auth;

public class RefreshTokenCommandHandler(
    IJwtTokenService jwtTokenService,
    IRefreshTokenService refreshTokenService)
    : IRequestHandler<RefreshTokenCommand, Result<string>>
{
    public async Task<Result<string>> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await refreshTokenService.ValidateAsync(request.dto.RefreshToken, cancellationToken);
            var jwt = jwtTokenService.GenerateAccessToken(user!);

            return Result.Ok(jwt);
        }
        catch (SecurityException)
        {
            return Result.Fail<string>("Invalid refresh token");
        }
    }
}
