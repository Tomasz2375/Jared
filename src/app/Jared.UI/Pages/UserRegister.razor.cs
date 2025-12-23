using Jared.Contracts.Auth;
using Jared.Dtos.Auth;
using Microsoft.AspNetCore.Components;

namespace Jared.UI.Pages;

public partial class UserRegister
{
    public RegisterRequestDto Dto { get; set; } = new();

    private async Task registerNewUser()
    {
        var result = await Mediator.Send(new RegisterCommand(Dto));
        if (!result.Success)
        {
            Console.WriteLine(result.Error);

            return;
        }

        NavigationManager.NavigateTo("login");
    }
}
