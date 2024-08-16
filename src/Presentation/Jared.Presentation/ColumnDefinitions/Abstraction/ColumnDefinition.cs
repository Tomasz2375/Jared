using Jared.Shared.Enums;
using System.Linq.Expressions;

namespace Jared.Presentation.ColumnDefinitions.Abstraction;

public class ColumnDefinition<T> : IColumnDefinition<T>
    where T : class
{
    public ColumnDefinition(
        string columnName,
        Type? type,
        Expression<Func<T,object?>> valueSelector,
        ColumnAlignment alignment,
        string width,
        string? format
    )
    {
        ColumnName = columnName;
        Type = type;
        ValueSelector = valueSelector;
        Alignment = alignment;
        Width = width;
        Format = format;
    }

    public Expression<Func<T, object?>> ValueSelector { get; set; }
    public string ColumnName {get; set; }
    public Type? Type { get; set; }
    public bool IsNullable { get; set; }
    public string? ParentPath { get; set; }
    public ColumnAlignment Alignment { get; set; }
    public string Width { get; set; }
    public string? Format { get; set; }

    public object? GetValue(object? obj)
    {
        return obj!.GetType();
    }

    LambdaExpression IColumnDefinition.ValueSelector => ValueSelector;
}
