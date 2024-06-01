using Jared.Domain.Abstractions;

namespace Jared.Domain.Models;

public class User : Entity
{
    public int RoleId { get; set; }

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateTime? DateOfBirth { get; set; }
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;

    public Role? Role { get; set; }
}
