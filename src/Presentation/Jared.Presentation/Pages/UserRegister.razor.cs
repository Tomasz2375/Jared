using Jared.Application.Dtos.UserDtos;
using Jared.Presentation.Requests.User.Register;
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
            Console.WriteLine("Register user failed");
        }

        NavigationManager.NavigateTo("login");
    }
}
