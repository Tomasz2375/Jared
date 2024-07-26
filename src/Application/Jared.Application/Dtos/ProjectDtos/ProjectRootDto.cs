using Jared.Application.Dtos.Abstractions;

namespace Jared.Application.Dtos.ProjectDtos;

public class ProjectRootDto : EntityDto<int>
{
    public string Title { get; set; } = default!;
    public string Code { get; set; } = default!;
}
