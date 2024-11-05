using Jared.Shared.Interfaces;

namespace Jared.Application.Services.Filters;

public class FilterBuilder<T> : IFilterBuilder<T>
    where T : class, IEntity
{
    public IFilter<T> Build(IFilterStrategy<T> strategy)
    {
        return new Filter<T>(strategy);
    }
}
