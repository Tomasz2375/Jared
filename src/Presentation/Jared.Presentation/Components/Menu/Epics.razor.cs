namespace Jared.Presentation.Components.Menu;

public partial class Epics
{
    private bool showUserMenu;
    private bool showCreateDialog;

    private string userMenuCssClass => showUserMenu ? "show-menu" : string.Empty;

    private void toggleUserMenu()
    {
        showUserMenu = !showUserMenu;
    }

    private async Task hideUserMemu()
    {
        await Task.Delay(100);
        showUserMenu = false;
    }
}
