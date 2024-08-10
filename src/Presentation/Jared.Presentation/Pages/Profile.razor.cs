using Jared.Application.Dtos.UserDtos;
using Jared.Presentation.Requests.User.Password;

namespace Jared.Presentation.Pages;

public partial class Profile
{
    private string message = string.Empty;
    private bool isPasswordChanged;
    private UserPasswordDto dto = new();

    private async Task changePassword()
    {
        var result = await Mediator.Send(new UserPasswordCommand(dto));

        if (!result.Success)
        {
            message = result.Error;
            isPasswordChanged = false;
        }
        else
        {
            message = "Password has been changed";
            isPasswordChanged = true;
        }
    }
}
