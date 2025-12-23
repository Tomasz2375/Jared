using System.Security.Claims;
using Jared.Application.Abstractions;
using Jared.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Jared.Application.Services;

public class UserService(IHttpContextAccessor httpContextAccessor) : IUserService
{
    public User GetUser()
    {
        var claimId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var claimName = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value;
        var claimRole = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;
        User user = new();
        if (int.TryParse(claimId, out int id))
        {
            user.Id = id;
        }

        if (!string.IsNullOrEmpty(claimName))
        {
            user.FirstName = claimName.Split(" ")[0];
            user.LastName = claimName.Split(" ")[1];
        }

        if (!string.IsNullOrEmpty(claimRole))
        {
            user.Role = new()
            {
                Name = claimRole,
            };
        }

        return user;
    }
}
