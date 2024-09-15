using Jared.Shared.Dtos.Abstractions;
using Jared.Shared.Enums;

namespace Jared.Shared.Dtos.EpicDtos;

public class EpicRootDto : EntityDto<int>
{
    public string Title { get; set; } = default!;
    public int? ParentId { get; set; }
    public int ProjectId { get; set; }
    public EpicStatus Status { get; set; }
}
