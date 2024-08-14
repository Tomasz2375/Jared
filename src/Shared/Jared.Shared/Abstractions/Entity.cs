using Jared.Shared.Interfaces;

namespace Jared.Shared.Abstractions;

public abstract class Entity : IEntity<int>
{
    public int Id { get; set; }

    object IEntity.Id
    {
        get
        {
            return Id;
        }
        set
        {
            if (value is not int)
            {
                throw new ArgumentException("Wrong Id type", nameof(value));
            }

            Id = (int)value;
        }
    }
}
