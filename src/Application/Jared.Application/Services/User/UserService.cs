using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Jared.Application.Services.User;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public Domain.Models.User GetUser()
    {
        var claimId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var claimName = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value;
        var claimRole = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;
        var claimDateOfBirth = httpContextAccessor.HttpContext?.User.FindFirst("DateOfBirth")?.Value;

        Domain.Models.User user = new();

        if (int.TryParse(claimId, out int id))
        {
            user.Id = id;
        }

        if (DateTime.TryParse(claimDateOfBirth, out DateTime dateOfBirth))
        {
            user.DateOfBirth = dateOfBirth;
        }

        if (claimName != null)
        {
            user.FirstName = claimName.Split(" ")[0];
            user.LastName = claimName.Split(" ")[1];
        }

        if (claimRole is not null)
        {
            user.Role = new()
            {
                Name = claimRole,
            };
        }

        return user;
    }
}
