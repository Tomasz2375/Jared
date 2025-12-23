using System.Security;
using System.Security.Cryptography;
using System.Text;
using Jared.Application.Abstractions;
using Jared.Domain.Abstractions;
using Jared.Domain.Models;
using Jared.Domain.Options;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Services;

public class RefreshTokenService(IDataContext dataContext, AuthenticationOptions options)
    : IRefreshTokenService
{
    public async Task<RefreshToken> CreateAsync(User user, CancellationToken cancellationToken)
    {
        var plainToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        RefreshToken refreshToken = new()
        {
            UserId = user.Id,
            Token = hash(plainToken),
            ExpiresAtUtc = DateTime.UtcNow.AddDays(options.JwtRefreeshExpireDays),
            CreatedAtUtc = DateTime.UtcNow,
        };

        dataContext.Set<RefreshToken>().Add(refreshToken);
        await dataContext.SaveChangesAsync(cancellationToken);

        refreshToken.Token = plainToken;

        return refreshToken;
    }

    public async Task<User?> ValidateAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var hashed = hash(refreshToken);
        var userRefreshToken = await dataContext
            .Set<RefreshToken>()
            .Include(x => x.User)
            .ThenInclude(x => x!.Role)
            .SingleOrDefaultAsync(x => x.Token == hashed, cancellationToken);
        if (userRefreshToken is not null && !userRefreshToken.IsActive)
        {
            throw new SecurityException("Invalid refresh token");
        }

        return userRefreshToken?.User;
    }

    private static string hash(string token)
    {
        using var sha256 = SHA256.Create();

        return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(token)));
    }
}
