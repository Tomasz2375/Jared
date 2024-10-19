using Jared.Shared.Dtos.UserDtos;

namespace Jared.Presentation.Services;

public interface IUserService
{
    string GetUserName();
    UserUpdateDto GetUserData();
}
