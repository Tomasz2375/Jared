namespace Jared.Dtos.Users;

public class UserListDto : BaseDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public int RoleId { get; set; }
}
