using Jared.Presentation.Requests.User.Password;
using Jared.Presentation.Requests.User.Update;
using Jared.Shared.Dtos.UserDtos;

namespace Jared.Presentation.Pages;

public partial class Profile
{
    private string passwordMessage = string.Empty;
    private string updateMessage = string.Empty;
    private bool isPasswordChanged;
    private bool isUpdateUser;
    private UserPasswordDto userPasswordDto = new();
    private UserUpdateDto userUpdateDto = new();

    protected override void OnInitialized()
    {
        userUpdateDto = UserService.GetUserData();
    }

    private async Task changePassword()
    {
        var result = await Mediator.Send(new UserPasswordCommand(userPasswordDto));

        if (!result.Success)
        {
            passwordMessage = result.Error;
            isPasswordChanged = false;
        }
        else
        {
            passwordMessage = "Password has been changed";
            isPasswordChanged = true;
        }
    }

    private async Task updateUser()
    {
        var result = await Mediator.Send(new UserUpdateCommand(userUpdateDto));

        if (!result.Success)
        {
            updateMessage = result.Error;
            isUpdateUser = false;
        }
        else
        {
            updateMessage = "User data updated";
            isUpdateUser = true;
        }
    }
}
