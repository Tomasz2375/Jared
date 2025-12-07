using Jared.UI.Components.Forms;

namespace Jared.UI.Components.Menu;

public partial class Projects
{
    private bool showUserMenu;

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

    private void createProject()
    {
        DialogService.Create<ProjectCreateForm>();
    }
}
