using System.Security.Claims;
using Jared.Client.Abstractions;
using Jared.Dtos.Users;
using Microsoft.AspNetCore.Components.Authorization;

namespace Jared.Client.Services;

public sealed class UserService(AuthenticationStateProvider authStateProvider) : IUserService
{
    public async Task<UserDto> GetUserAsync()
    {
        var state = await authStateProvider.GetAuthenticationStateAsync();
        var user = state.User;
        if (!user.Identity?.IsAuthenticated ?? false)
        {
            return new();
        }

        return new()
        {
            Id = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value),
            FullName = user.FindFirst(ClaimTypes.Name)!.Value,
            Email = user.FindFirst(ClaimTypes.Email)!.Value,
            Role = user.FindFirst(ClaimTypes.Role)!.Value,
        };
    }
}
