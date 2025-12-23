using Jared.Dtos.Auth;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;

namespace Jared.UI.Pages;

public partial class UserLogin
{
    private LoginRequestDto dto { get; set; } = new();
    private string errorMessage = string.Empty;
    private string returnUrl = string.Empty;

    protected override void OnInitialized()
    {
        var uri = Nav.ToAbsoluteUri(Nav.Uri);
        if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("returnUrl", out var url))
        {
            returnUrl = url!;
        }
    }

    private async Task loginUser()
    {
        var ok = await JS.InvokeAsync<bool>("auth.login", dto);

        if (ok)
        {
            Nav.NavigateTo(returnUrl, true);

            return;
        }

        errorMessage = "Login failed";
    }
}
