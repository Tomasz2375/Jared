using Jared.Dtos.Users;
using Microsoft.JSInterop;

namespace Jared.UI.Components.Menu;

public partial class UserButton
{
    private bool showUserMenu = false;
    private UserDto user = default!;
    private string userMenuCssClass => showUserMenu ? "show-menu" : string.Empty;

    protected override async Task OnInitializedAsync()
    {
        user = await UserService.GetUserAsync();
    }

    private void toggleUserMenu()
    {
        showUserMenu = !showUserMenu;
    }

    private async Task hideUserMemu()
    {
        await Task.Delay(100);
        showUserMenu = false;
    }

    private async Task logout()
    {
        var subbess = await JSRuntime.InvokeAsync<bool>("auth.logout");
        if (subbess)
        {
            NavigationManager.NavigateTo("/login", forceLoad: true);
        }
    }
}
