using Jared.Contracts.Users;
using Jared.Dtos.Users;
using Microsoft.AspNetCore.Components;

namespace Jared.UI.Pages;

public partial class Profile
{
    [Parameter]
    public int Id { get; set; }

    private string passwordMessage = string.Empty;
    private string updateMessage = string.Empty;
    private bool isPasswordChanged;
    private bool isUpdateUser;
    private UserPasswordDto userPasswordDto = new();
    private UserDetailsDto userUpdateDto = new();

    protected override async Task OnInitializedAsync()
    {
        var result = await Mediator.Send(new UserDetailsQuery(Id));
        if (!result.Success)
        {
            return;
        }

        userUpdateDto = result.Data;
        userPasswordDto.Id = Id;
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
