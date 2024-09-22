using Jared.Shared.Interfaces;

namespace Jared.Application.Services.Filters;

public class Filter<T> : IFilter<T> where T : class, IEntity
{
    private readonly IFilterStrategy<T> filter;

    public Filter(IFilterStrategy<T> filter)
    {
        this.filter = filter;
    }

    public IQueryable<T> ApplyFilters(IQueryable<T> query, string key, string value)
    {
        return filter.ApplyFilter(query, key, value);
    }
}
