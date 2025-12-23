namespace Jared.Dtos.Users;

public class UserDto : BaseDto
{
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Role { get; set; } = default!;
}
