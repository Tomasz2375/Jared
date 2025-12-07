namespace Jared.Dtos.Users;

public class UserDetailsDto : BaseDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateTime? DateOfBirth { get; set; }
}
