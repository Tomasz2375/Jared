using System.Linq.Expressions;

namespace Jared.Presentation.ColumnDefinitions.Abstraction;

public interface IColumnDefinition<T> : IColumnDefinition
{
    new Expression<Func<T, object?>> ValueSelector { get; }
}

public interface IColumnDefinition
{
    string ColumnName { get; }
    LambdaExpression ValueSelector { get; }
    object? GetValue(object? obj);
    Type? Type { get; set; }
    bool IsNullable { get; set; }
    string? ParentPath { get; set; }
}
