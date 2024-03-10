using Jared.Application.Dtos.Abstractions;
using Jared.Presentation.ColumnDefinitions;
using Jared.Presentation.ColumnDefinitions.Abstraction;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components;

public partial class DataGrid<TItem>
{
    [Parameter]
    public List<TItem> Items { get; set; } = new();
    [Parameter]
    public IEnumerable<IColumnDefinition<TItem>> ItemsDefinition { get; set; }
    [Parameter]
    public IPagination Pagination { get; set; }
    [Parameter]
    public Query Query { get; set; }
    [Parameter]
    public EventCallback<Query> SendPageQuery { get; set; }

    private void previousPage()
    {
        if (Query.Page == 1)
        {
            return;
        }

        Query.Page--;
        SendPageQuery.InvokeAsync(Query);
    }
    private void nextPage()
    {
        if (Query.Page == Pagination.PageCount)
        {
            return;
        }

        Query.Page++;
        SendPageQuery.InvokeAsync(Query);
    }

}
