using Jared.Domain.Abstractions;

namespace Jared.Application.Services.Filters;

public interface IFilter<T>
    where T : Entity
{
    IQueryable<T> ApplyFilters(IQueryable<T> query, string key, string value);
}
