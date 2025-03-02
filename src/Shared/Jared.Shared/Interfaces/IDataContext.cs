using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Jared.Shared.Interfaces;

public interface IDataContext : IDisposable
{
    EntityEntry Entry(object entity);
    EntityEntry Add(object entities);
    void AddRange(IEnumerable<object> entities);
    EntityEntry Remove(object entity);
    void RemoveRange(IEnumerable<object> entities);

    IModel Model { get; }
    DatabaseFacade Database { get; }
    ChangeTracker ChangeTracker { get; }
    Task<int> SaveChangesAsync(CancellationToken token);
    DbSet<TEntity> Set<TEntity>()
        where TEntity : class, IEntity;
}
