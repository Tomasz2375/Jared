using Jared.Shared.Dtos.UserDtos;

namespace Jared.Presentation.Services;

public interface IUserService
{
    int GetUserId();
    string GetUserRole();
    string GetUserName();
    UserUpdateDto GetUserData();
}
