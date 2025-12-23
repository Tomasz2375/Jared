using Jared.Dtos.Users;

namespace Jared.Client.Abstractions;

public interface IUserService
{
    Task<UserDto> GetUserAsync();
}
