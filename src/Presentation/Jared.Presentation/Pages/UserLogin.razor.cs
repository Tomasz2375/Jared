using Jared.Application.Dtos.UserDtos;
using Jared.Presentation.CQRS.User.Login;

namespace Jared.Presentation.Pages;

public partial class UserLogin
{
    public UserLoginDto Dto { get; set; } = new();
    private string errorMessage = string.Empty;

    private async Task loginUser()
    {
        var result = await Mediator.Send(new UserLoginCommand(Dto));
        if (result.Success)
        {
            errorMessage = string.Empty;
            await LocalStorage.SetItemAsync("authToken", result.Data);
            await AuthenticationStateProvider.GetAuthenticationStateAsync();
            NavigationManager.NavigateTo("");
        }

        errorMessage = result.Error;
    }
}
