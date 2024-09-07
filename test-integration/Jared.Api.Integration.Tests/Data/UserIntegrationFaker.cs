using Jared.Domain.Models;

namespace Jared.Api.Integration.Tests.Data;

public class UserIntegrationFaker : BaseIntegrationFaker<User>
{
    protected override int PRIORITY => 2;

    public static User FirstUser => new()
    {
        Id = BASE_ID + 1,
        RoleId = RoleIntegrationFaker.UserRole.Id,
        FirstName = "FIRST_USER_NAME",
        LastName = "FIRST_USER_SURNANE",
        DateOfBirth = new(1990, 5, 25),
        Email = "FIRST_USER@EMAIL.COM",
        PasswordHash = "FIRST_USER_PASSWORD_HASH",
    };

    public static User SecondUser => new()
    {
        Id = BASE_ID + 2,
        RoleId = RoleIntegrationFaker.ManagerRole.Id,
        FirstName = "SECOND_USER_NAME",
        LastName = "SECOND_USER_SURNANE",
        DateOfBirth = new(1992, 7, 8),
        Email = "SECOND_USER@EMAIL.COM",
        PasswordHash = "SECOND_USER_PASSWORD_HASH",
    };

    public static User ThirdUser => new()
    {
        Id = BASE_ID + 3,
        RoleId = RoleIntegrationFaker.AdminRole.Id,
        FirstName = "THIRD_USER_NAME",
        LastName = "THIRD_USER_SURNANE",
        DateOfBirth = new(1993, 9, 29),
        Email = "THIRD_USER@EMAIL.COM",
        PasswordHash = "THIRD_USER_PASSWORD_HASH",
    };
}
