namespace Jared.Presentation.ColumnDefinitions.Abstraction;

public abstract class ListDefinitionBase<T> : IListDefinition<T>
{
    public abstract IEnumerable<IColumnDefinition<T>> GetColumns();

    IEnumerable<IColumnDefinition> IListDefinition.GetColumns()
    {
        return GetColumns();
    }
}
