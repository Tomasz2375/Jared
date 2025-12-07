using Jared.Contracts.Projects;
using Jared.UI.Components.Forms;

namespace Jared.UI.Components.Menu;

public partial class Tasks
{
    private Dictionary<string, string> projectsDictionary = new();

    protected override async Task OnInitializedAsync()
    {
        await getProjectsAsync();
    }

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

    private async Task getProjectsAsync()
    {
        var result = await Mediator.Send(new ProjectPageQuery());

        if (!result.Success)
        {
            Console.WriteLine("Error when get project list");
            return;
        }

        var projects = result.Data.Items;
        projectsDictionary = projects.ToDictionary(x => x.Id.ToString(), x => x.Title);
    }

    private void createTask()
    {
        DialogService.Create<TaskCreateForm>();
    }
}
