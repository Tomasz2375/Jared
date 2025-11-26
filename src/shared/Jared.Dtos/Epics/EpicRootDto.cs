using Jared.Shared.Enums;

namespace Jared.Dtos.Epics;

public class EpicRootDto : BaseDto
{
    public string Title { get; set; } = default!;
    public int? ParentId { get; set; }
    public int ProjectId { get; set; }
    public EpicStatus Status { get; set; }
}
