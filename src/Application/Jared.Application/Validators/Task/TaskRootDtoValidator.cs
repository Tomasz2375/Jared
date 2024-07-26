using FluentValidation;
using Jared.Application.Dtos.TaskDtos;

namespace Jared.Application.Validators.Task;

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
