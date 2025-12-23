using Jared.Domain.Models;

namespace Jared.Application.Abstractions;

public interface IRefreshTokenService
{
    Task<RefreshToken> CreateAsync(User user, CancellationToken cancellationToken);
    Task<User?> ValidateAsync(string refreshToken, CancellationToken cancellationToken);
}
