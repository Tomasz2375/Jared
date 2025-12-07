using Jared.Dtos.TaskHistories;
using Microsoft.AspNetCore.Components;

namespace Jared.UI.Components.Forms;

public partial class TaskDetailsHistory
{
    [Parameter]
    public List<TaskHistoryDto> TaskHistoryDtos { get; set; } = default!;
}
