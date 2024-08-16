using Jared.Shared.Enums;
using Jared.Presentation.ColumnDefinitions.Abstraction;

namespace Jared.Presentation.ColumnDefinitions;

public class Query : IQuery
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string? SortingProperty { get; set; }
    public SortingDirection? SortingDirection { get; set; }
    public Dictionary<string, string?>? Filter { get; set; } = new();
}
