﻿using Jared.Application.Dtos.Abstractions;
using Jared.Domain.Enums;
using Jared.Domain.Interfaces;
using Jared.Presentation.ColumnDefinitions;
using Jared.Presentation.ColumnDefinitions.Abstraction;
using Jared.Presentation.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components;

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
    public TaskDetailsForm TaskDetailsForm { get; set; } = default!;
    
    private int showDetailsId;
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
}
