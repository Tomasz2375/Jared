using Jared.Shared.Interfaces;

namespace Jared.Application.Services.Filters;

public interface IFilter<T>
    where T : class, IEntity
{
    IQueryable<T> ApplyFilters(IQueryable<T> query, string key, string value);
}
