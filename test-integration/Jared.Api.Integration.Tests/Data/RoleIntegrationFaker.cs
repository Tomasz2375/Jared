using Jared.Domain.Models;

namespace Jared.Api.Integration.Tests.Data;

public class RoleIntegrationFaker : BaseIntegrationFaker<Role>
{
    protected override int PRIORITY => 1;

    public static Role UserRole => new()
    {
        Id = BASE_ID + 1,
        Name = "User",
    };

    public static Role ManagerRole => new()
    {
        Id = BASE_ID + 2,
        Name = "Manager",
    };

    public static Role AdminRole => new()
    {
        Id = BASE_ID + 3,
        Name = "Admin",
    };
}
