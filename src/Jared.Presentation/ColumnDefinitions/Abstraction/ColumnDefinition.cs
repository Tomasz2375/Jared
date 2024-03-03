﻿using System.Linq.Expressions;

namespace Jared.Presentation.ColumnDefinitions.Abstraction;

public class ColumnDefinition<T> : IColumnDefinition<T>
    where T : class
{
    public ColumnDefinition(string columnName, Expression<Func<T, object?>> valueSelector)
    {
        ColumnName = columnName;
        ValueSelector = valueSelector;
    }

    public Expression<Func<T, object?>> ValueSelector { get; set; }
    public string ColumnName {get; set; }
    public Type? Type { get; set; }
    public bool IsNullable { get; set; }
    public string? ParentPath { get; set; }

    public object? GetValue(object? obj)
    {
        return obj!.GetType();
    }

    LambdaExpression IColumnDefinition.ValueSelector => ValueSelector;
}
