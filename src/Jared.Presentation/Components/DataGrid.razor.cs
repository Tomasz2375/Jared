using Jared.Application.Dtos.TaskDto;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components;

public partial class DataGrid<TItem>
{
    [Parameter]
    public List<TItem> Items { get; set; } = new();
}
