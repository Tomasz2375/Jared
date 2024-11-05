using Jared.Presentation.Requests.User.Register;
using Jared.Shared.Dtos.UserDtos;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Pages;

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
