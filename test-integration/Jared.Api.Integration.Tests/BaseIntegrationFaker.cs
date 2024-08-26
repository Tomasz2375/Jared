using Jared.Shared.Interfaces;

namespace Jared.Api.Integration.Tests;

public abstract class BaseIntegrationFaker<TEntity>
    where TEntity : class, IEntity
{
    protected static int BASE_ID { get; set; } = 1000;

    protected abstract int PRIORITY { get; }
}
