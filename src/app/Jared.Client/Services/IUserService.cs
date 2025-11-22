using Jared.Dtos.Users;

namespace Jared.Client.Services;

public interface IUserService
{
    int GetUserId();
    string GetUserRole();
    string GetUserName();
    UserUpdateDto GetUserData();
}
