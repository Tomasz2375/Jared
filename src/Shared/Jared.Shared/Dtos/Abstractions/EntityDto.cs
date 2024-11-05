using Jared.Shared.Interfaces;

namespace Jared.Shared.Dtos.Abstractions;

public abstract class EntityDto<TKey> : IEntity<TKey>
{
    public TKey Id { get; set; } = default!;

    object IEntity.Id
    {
        get
        {
            return Id!;
        }
        set
        {
            if (value is not TKey key)
            {
                throw new ArgumentException("Wrong entity Id type");
            }

            Id = key;
        }
    }
}
