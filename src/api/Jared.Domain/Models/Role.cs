using Jared.Domain.Abstractions;

namespace Jared.Domain.Models;

public class Role : Entity
{
    public string Name { get; set; } = default!;

    public List<User> Users { get; set; } = new();
}
