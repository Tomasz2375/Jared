using FluentValidation;
using Jared.Dtos.Projects;

namespace Jared.Validators.Project;

public class ProjectRootDtoValidator : AbstractValidator<ProjectRootDto>
{
    public ProjectRootDtoValidator()
    {
        RuleFor(x => x.Color)
            .NotEmpty()
            .Matches(@"^#([A-Fa-f0-9]{6})$")
            .WithMessage("Wymagany format to '#RRGGBB'");
    }
}
