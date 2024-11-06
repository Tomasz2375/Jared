using Jared.Shared.Interfaces;

namespace Jared.Application.Services.Filters;

public class Filter<T>(IFilterStrategy<T> filter) : IFilter<T>
    where T : class, IEntity
{
    private readonly IFilterStrategy<T> filter = filter;

    public IQueryable<T> ApplyFilters(IQueryable<T> query, string key, string value)
    {
        return filter.ApplyFilter(query, key, value);
    }
}
