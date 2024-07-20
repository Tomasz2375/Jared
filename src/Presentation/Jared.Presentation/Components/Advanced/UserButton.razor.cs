namespace Jared.Presentation.Components.Advanced;

public partial class UserButton
{
    private bool showUserMenu = false;

    private string userMenuCssClass => showUserMenu ? "show-menu" : string.Empty;

    private void toggleUserMenu()
    {
        showUserMenu = !showUserMenu;
    }

    private async Task hideUserMemu()
    {
        await Task.Delay(200);
        showUserMenu = false;
    }

    private async Task logout()
    {
        await LocalStorage.RemoveItemAsync("auhtToken");
        await AuthenticationStateProvider.GetAuthenticationStateAsync();

        NavigationManager.NavigateTo("");
    }
}
