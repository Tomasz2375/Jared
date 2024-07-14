using Jared.Application.Commands.UserCommands;
using Jared.Application.Dtos.UserDtos;

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
    }
}
