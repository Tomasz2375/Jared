using Jared.Shared.Interfaces;

namespace Jared.Application.Services.Filters;

public interface IFilterBuilder<T>
    where T : class, IEntity
{
    IFilter<T> Build(IFilterStrategy<T> strategy);
}
