namespace Jared.Dtos.Projects;

public class ProjectRootDto : BaseDto
{
    public string Title { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string Color { get; set; } = default!;
}
