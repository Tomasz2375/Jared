using FluentValidation;
using Jared.Shared.Dtos.ProjectDtos;

namespace Jared.Shared.Validators.Project;

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
