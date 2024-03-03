using Jared.Application.Dtos.TaskDto;
using Jared.Presentation.ColumnDefinitions.Abstraction;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components;

public partial class DataGrid<TItem>
{
    [Parameter]
    public List<TItem> Items { get; set; } = new();
    [Parameter]
    public IEnumerable<IColumnDefinition<TaskListDto>> ItemsDefinition { get; set; }
}
