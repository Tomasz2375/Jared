using Jared.Domain.Abstractions;

namespace Jared.Application.Services.Filters;

public interface IFilterBuilder<T>
    where T : Entity
{
    IFilter<T> Build(IFilterStrategy<T> strategy);
}
