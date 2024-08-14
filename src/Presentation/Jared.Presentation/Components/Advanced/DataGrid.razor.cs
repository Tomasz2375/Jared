using Jared.Application.Dtos.Abstractions;
using Jared.Shared.Enums;
using Jared.Shared.Interfaces;
using Jared.Presentation.ColumnDefinitions;
using Jared.Presentation.ColumnDefinitions.Abstraction;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components.Advanced;

public partial class DataGrid<TItem> where TItem : class, IEntity
{
    [Parameter]
    [EditorRequired]
    public string Title { get; set; } = default!;
    [Parameter]
    [EditorRequired]
    public IEnumerable<TItem> Items { get; set; } = default!;
    [Parameter]
    [EditorRequired]
    public IEnumerable<IColumnDefinition<TItem>> ItemsDefinition { get; set; } = default!;
    [Parameter]
    [EditorRequired]
    public IPagination Pagination { get; set; } = default!;
    [Parameter]
    [EditorRequired]
    public Query Query { get; set; } = default!;
    [Parameter]
    [EditorRequired]
    public EventCallback<Query> SendPageQuery { get; set; }
    [Parameter]
    public bool SwitchPagination { get; set; }
    [Parameter]
    public EventCallback<int> ShowDialog { get; set; }

    private List<int> pageSizes = new()
    {
        5, 10, 20, 30, 50, 100,
    };

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
    private void showFilterResult()
    {
        Query.Page = 1;
        SendPageQuery.InvokeAsync(Query);
    }

    private string sortCssClass(string columnName)
    {
        if (Query.SortingProperty is null || columnName.ToLower() != Query.SortingProperty!.ToLower())
        {
            return string.Empty;
        }

        return Query.SortingDirection == SortingDirection.Descending ?
            "arrow arrow-down" :
            "arrow arrow-up";
    }

    private void sortByColumn(string columnName)
    {
        if (Query.SortingProperty is null || columnName.ToLower() != Query.SortingProperty!.ToLower())
        {
            Query.SortingProperty = columnName;
            Query.SortingDirection = SortingDirection.Ascending;
        }
        else
        {
            Query.SortingDirection = 
                Query.SortingDirection == SortingDirection.Ascending ?
                SortingDirection.Descending :
                SortingDirection.Ascending;
        }

        SendPageQuery.InvokeAsync(Query);
    }

    private void pageSize()
    {
        Query.Page = 1 + Pagination.ItemFrom / Query.PageSize;

        SendPageQuery.InvokeAsync(Query);
    }

    private object? showValue(TItem item, IColumnDefinition<TItem> column)
    {
        var property = item.GetType().GetProperty(column.ColumnName)!;
        if (property.GetValue(item) is null)
        {
            return null;
        }
        if (column.Format is null)
        {
            return property.GetValue(item)!;
        }

        if (property.PropertyType == typeof(DateTime?) || property.PropertyType == typeof(DateTime))
        {
            var date = (DateTime)property.GetValue(item)!;
            return date.ToString(column.Format);
        }
        else
        {
            return property.GetValue(item)!;
        }
    }

    private string createDictionary(IColumnDefinition<TItem> column)
    {
        if (Query.Filter!.ContainsKey(column.ColumnName))
        {
            return column.ColumnName;
        }

        if (column.Type == typeof(DateTime) || column.Type == typeof(DateTime?))
        {
            Query.Filter!.Add(column.ColumnName, "-");
        }
        else
        {
            Query.Filter!.Add(column.ColumnName, null);
        }

        return column.ColumnName;
    }

    public void updateFilters(string key, string value)
    {
        if (string.IsNullOrEmpty(key) || !Query.Filter!.ContainsKey(key))
        {
            return;
        }

        Query.Filter[key] = value;
        showFilterResult();
    }
}
