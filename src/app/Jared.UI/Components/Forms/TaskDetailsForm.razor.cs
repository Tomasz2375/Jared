using Jared.Client.Commons;
using Jared.Client.Requests.Tasks.Details;
using Jared.Client.Requests.Tasks.Update;
using Jared.Dtos.Tasks;
using Microsoft.AspNetCore.Components;

namespace Jared.UI.Components.Forms;

public partial class TaskDetailsForm
{
    [Parameter]
    public int Id { get; set; }

    public TaskDetailsDto Dto { get; set; } = new();
    private int tabNumber;
    private bool closeDialog;

    protected override async Task OnInitializedAsync()
    {
        await getDetails(Id);
    }

    private void cancel()
    {
        NotificationService.Information(NotificationHelper.TASK_UPDATE_CANCELED);
        Close();
    }

    private async Task save()
    {
        var result = await Mediator.Send(new TaskUpdateCommand(Dto));
        if (!result.Success)
        {
            NotificationService.Error(NotificationHelper.TASK_UPDATE_FAILED);
        }

        NotificationService.Success(NotificationHelper.TASK_UPDATE_SUCCESS);
        if (closeDialog)
        {
            Close();
            return;
        }

        await getDetails(Id);
    }

    private async Task getDetails(int id)
    {
        var result = await Mediator.Send(new TaskDetailsQuery(id));

        if (!result.Success)
        {
            Console.WriteLine("Get task details failed");
        }

        Dto = result.Data;
    }
}
