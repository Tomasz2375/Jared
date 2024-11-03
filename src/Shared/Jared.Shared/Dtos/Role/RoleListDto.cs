using Jared.Shared.Dtos.Abstractions;

namespace Jared.Shared.Dtos.Role;

public class RoleListDto : EntityDto<int>
{
    public string Name { get; set; } = default!;
}
