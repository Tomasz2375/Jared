using Jared.Shared.Dtos.TaskDtos;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components.Forms;

public partial class TaskDetailsWorkLog
{
    [Parameter]
    public TaskDetailsDto Dto { get; set; } = default!;
}
