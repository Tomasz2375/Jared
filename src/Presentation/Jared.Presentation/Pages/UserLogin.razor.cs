using Jared.Application.Dtos.UserDtos;
using Jared.Presentation.CQRS.User.Login;
using Microsoft.AspNetCore.WebUtilities;

namespace Jared.Presentation.Pages;

public partial class UserLogin
{
    public UserLoginDto Dto { get; set; } = new();
    private string errorMessage = string.Empty;
    private string returnUrl = string.Empty;

    protected override void OnInitialized()
    {
        var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

        if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("returnUrl", out var url))
        {
            returnUrl = url;
        }
    }

    private async Task loginUser()
    {
        var result = await Mediator.Send(new UserLoginCommand(Dto));
        if (result.Success)
        {
            errorMessage = string.Empty;
            await LocalStorage.SetItemAsync("authToken", result.Data);
            await AuthenticationStateProvider.GetAuthenticationStateAsync();
            NavigationManager.NavigateTo(returnUrl);
        }

        errorMessage = result.Error;
    }
}
