using Jared.Shared.Dtos.Abstractions;

namespace Jared.Shared.Dtos.ProjectDtos;

public class ProjectRootDto : EntityDto<int>
{
    public string Title { get; set; } = default!;
    public string Code { get; set; } = default!;
}
