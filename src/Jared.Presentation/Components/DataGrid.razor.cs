using Jared.Application.Dtos.Abstractions;
using Jared.Presentation.ColumnDefinitions;
using Jared.Presentation.ColumnDefinitions.Abstraction;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components;

public partial class DataGrid<TItem>
{
    [Parameter]
    [EditorRequired]
    public IEnumerable<TItem> Items { get; set; }
    [Parameter]
    [EditorRequired]
    public IEnumerable<IColumnDefinition<TItem>> ItemsDefinition { get; set; }
    [Parameter]
    [EditorRequired]
    public IPagination Pagination { get; set; }
    [Parameter]
    [EditorRequired]
    public Query Query { get; set; }
    [Parameter]
    [EditorRequired]
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
