using Jared.Application.Dtos.TaskDto;
using Jared.Application.Queries.TaskQueries;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Pages;

public partial class Task : ComponentBase
{
    public List<TaskListDto> Model { get; set; } = new();

    protected override async System.Threading.Tasks.Task OnInitializedAsync()
    {
        var result = await Mediator.Send(new TaskListQuery());

        if (!result.Success)
        {
            return;
        }

        Model = result.Data;
    }
}
