using Jared.Application.Dtos.TaskHistoryDtos;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components.Forms;

public partial class TaskDetailsHistory
{
    [Parameter]
    public List<TaskHistoryDto> TaskHistoryDtos { get; set; } = default!;
}
