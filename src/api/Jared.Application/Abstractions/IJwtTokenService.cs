using Jared.Domain.Models;

namespace Jared.Application.Abstractions;

public interface IJwtTokenService
{
    string GenerateAccessToken(User user);
}
