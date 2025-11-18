using Jared.Shared.Interfaces;

namespace Jared.Application.Services.Filters;

public interface IFilterStrategy<T>
    where T : class, IEntity
{
    IQueryable<T> ApplyFilter(IQueryable<T> query, string key, string value);
}
