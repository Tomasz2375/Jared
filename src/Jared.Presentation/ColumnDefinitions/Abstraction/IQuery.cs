using Jared.Domain.Enums;

namespace Jared.Presentation.ColumnDefinitions.Abstraction;

public interface IQuery
{
    public int Page {  get; set; }
    public int PageSize { get; set; }
    public string? Filter { get; set; }
    public string? SortingProperty { get; set; }
    public SortingDirection? SortingDirection { get; set; }
}
