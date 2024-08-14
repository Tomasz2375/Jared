using FluentValidation;
using Jared.Shared.Dtos.TaskDtos;

namespace Jared.Shared.Validators.Task;

public class TaskRootDtoValidator : AbstractValidator<TaskRootDto>
{
    public TaskRootDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.ProjectId).NotNull().GreaterThan(0);
        RuleFor(x => x.Status).IsInEnum();
        RuleFor(x => x.Priority).IsInEnum();
    }
}
