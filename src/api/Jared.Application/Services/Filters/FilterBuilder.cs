using Jared.Domain.Abstractions;

namespace Jared.Application.Services.Filters;

public class FilterBuilder<T> : IFilterBuilder<T>
    where T : Entity
{
    public IFilter<T> Build(IFilterStrategy<T> strategy)
    {
        return new Filter<T>(strategy);
    }
}
