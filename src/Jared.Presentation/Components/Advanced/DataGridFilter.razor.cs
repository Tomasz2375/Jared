using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components.Advanced;

public partial class DataGridFilter
{
    [Parameter]
    public string Id { get; set; } = default!;
    [Parameter]
    public string? Filter { get; set; }
    [Parameter]
    public Type Type { get; set; } = default!;
    [Parameter]
    public string Size { get; set; } = default!;
    [Parameter]
    public EventCallback<string> OnFilterChange { get; set; }

    public string? Value
    {
        get => Filter;
        set
        {
            if (value != Filter)
            {
                Filter = value;
                OnFilterChange.InvokeAsync(Filter);
            }
        }
    }

    public DateTime? DateFrom
    {
        get
        {
            if (string.IsNullOrEmpty(Filter!.Split('-')[0]))
            {
                return null;
            }
            else
            {
                return DateTime.Parse(Filter!.Split('-')[0]);
            }
        }
        set
        {
            Filter = createDateFilter(value, DateTo);
            OnFilterChange.InvokeAsync(Filter);
        }
    }

    public DateTime? DateTo
    {
        get
        {
            if (string.IsNullOrEmpty(Filter!.Split('-')[1]))
            {
                return null;
            }
            else
            {
                return DateTime.Parse(Filter!.Split('-')[1]);
            }
        }
        set
        {
            Filter = createDateFilter(DateFrom, value);
            OnFilterChange.InvokeAsync(Filter);
        }
    }

    public string? SelectedValue
    {
        get
        {
            return Filter;
        }
        set
        {
            if (string.IsNullOrEmpty(value) || value == "0")
            {
                Filter = "0";
            }
            else
            {
                if ((int.Parse(value!) & int.Parse(Filter!)) == 0)
                {
                    Filter = (int.Parse(Filter!) + int.Parse(value!)).ToString();
                }
                else
                {
                    Filter = (int.Parse(Filter!) - int.Parse(value!)).ToString();
                }
            }
            OnFilterChange.InvokeAsync(Filter);
        }
    }

    private string createDateFilter(DateTime? dateFrom, DateTime? dateTo)
    {
        string from = string.Empty;
        string to = string.Empty;

        if (dateFrom is not null)
        {
            from = ((DateTime)dateFrom).ToString();
        }
        if (dateTo is not null)
        {
            to = ((DateTime)dateTo).ToString();
        }

        return from + "-" + to;
    }

    private string colorSelectedEnum(object item)
    {
        if (string.IsNullOrEmpty(Filter))
        {
            Filter = "0";
        }
        foreach (var value in Type.GetEnumValues())
        {
            if (((int)item & int.Parse(Filter)) != 0)
            {
                return "edit-select";
            }
        }
        return string.Empty;
    }
}
