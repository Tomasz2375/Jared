namespace Jared.Client.ColumnDefinitions.Abstraction;

public interface IListDefinition<T> : IListDefinition
{
    new IEnumerable<IColumnDefinition<T>> GetColumns();
}

public interface IListDefinition
{
    IEnumerable<IColumnDefinition> GetColumns();
}
