using Jared.Shared.Enums;
using System.Linq.Expressions;

namespace Jared.Presentation.ColumnDefinitions.Abstraction;

public class ColumnDefinition<T>(
    string columnName,
    Type? type,
    Expression<Func<T, object?>> valueSelector,
    ColumnAlignment alignment,
    string width,
    string? format)
    : IColumnDefinition<T>
        where T : class
{
    public Expression<Func<T, object?>> ValueSelector { get; set; } = valueSelector;
    public string ColumnName { get; set; } = columnName;
    public Type? Type { get; set; } = type;
    public bool IsNullable { get; set; }
    public string? ParentPath { get; set; }
    public ColumnAlignment Alignment { get; set; } = alignment;
    public string Width { get; set; } = width;
    public string? Format { get; set; } = format;

    public object? GetValue(object? obj)
    {
        return obj!.GetType();
    }

    LambdaExpression IColumnDefinition.ValueSelector => ValueSelector;
}
