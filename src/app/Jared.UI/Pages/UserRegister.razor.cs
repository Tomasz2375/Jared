using Jared.Client.Requests.User.Register;
using Jared.Dtos.Users;
using Microsoft.AspNetCore.Components;

namespace Jared.UI.Pages;

public partial class UserRegister
{
    public UserRegisterDto Dto { get; set; } = new();

    private async Task registerNewUser()
    {
        var result = await Mediator.Send(new UserRegisterCommand(Dto));
        if (!result.Success)
        {
            Console.WriteLine(result.Error);

            return;
        }

        NavigationManager.NavigateTo("login");
    }
}
