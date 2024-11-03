using Jared.Shared.Dtos.Abstractions;

namespace Jared.Shared.Dtos.UserDtos;

public class UserListDto : EntityDto<int>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public int RoleId { get; set; }
}
