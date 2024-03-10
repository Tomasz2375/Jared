using Jared.Domain.Enums;
using Jared.Presentation.ColumnDefinitions.Abstraction;

namespace Jared.Presentation.ColumnDefinitions
{
    public class Query : IQuery
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string? Filter { get; set; }
        public string? SortingProperty { get; set; }
        public SortingDirection? SortingDirection { get; set; }
    }
}
