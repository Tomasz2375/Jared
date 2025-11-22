using Jared.Domain.Abstractions;

namespace Jared.Api.Integration.Tests;

public abstract class BaseIntegrationFaker<TEntity>
    where TEntity : Entity
{
    protected static int BASE_ID { get; set; } = 1000;

    protected abstract int PRIORITY { get; }
}
