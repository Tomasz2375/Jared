using Jared.Application.Commands.TaskCommand;
using Jared.Application.Dtos.TaskDtos;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components.Forms;

public partial class TaskCreateForm
{
    [Parameter]
    public EventCallback CloseDialog { get; set; }

    [Parameter]
    public string? Title { get; set; }

    public TaskDetailsDto Dto { get; set; } = new();

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Dto.Deadline = DateTime.Now.Date;
        Dto.Priority = Domain.Enums.Priority.Normal;
        Dto.Status = Domain.Enums.TaskStatus.ToDo;
    }

    private void cancel()
    {
        CloseDialog.InvokeAsync();
    }

    private async Task save()
    {
        var result = await Mediator.Send(new TaskCreateCommand(Dto));
        if (!result.Success)
        {
            Console.WriteLine("Create task failed");
        }

        await CloseDialog.InvokeAsync();
    }
}