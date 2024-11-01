using Jared.Shared.Dtos.Abstractions;

namespace Jared.Shared.Dtos.UserDtos
{
    public class UserRoleUpdateDto : EntityDto<int>
    {
        public int RoleId { get; set; }
    }
}
