using Jared.Contracts.Users;
using Jared.Dtos.Users;
using Microsoft.AspNetCore.Components;

namespace Jared.UI.Pages;

public partial class Profile
{
    [Parameter]
    public int Id { get; set; }

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
            NotificationService.Error(result.Error);

            return;
        }

        NotificationService.Success("Password has been changed");
    }

    private async Task updateUser()
    {
        var result = await Mediator.Send(new UserUpdateCommand(userUpdateDto));

        if (!result.Success)
        {
            NotificationService.Error(result.Error);

            return;
        }

        NotificationService.Success("User data updated");
    }
}
