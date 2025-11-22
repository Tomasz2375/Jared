using Jared.Domain.Abstractions;

namespace Jared.Application.Services.Filters;

public class Filter<T>(IFilterStrategy<T> filter) : IFilter<T>
    where T : Entity
{
    private readonly IFilterStrategy<T> filter = filter;

    public IQueryable<T> ApplyFilters(IQueryable<T> query, string key, string value)
    {
        return filter.ApplyFilter(query, key, value);
    }
}
