using Jared.Domain.Abstractions;

namespace Jared.Application.Services.Filters;

public interface IFilterStrategy<T>
    where T : Entity
{
    IQueryable<T> ApplyFilter(IQueryable<T> query, string key, string value);
}
