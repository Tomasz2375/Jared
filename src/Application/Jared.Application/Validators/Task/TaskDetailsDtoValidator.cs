using FluentValidation;
using Jared.Shared.Dtos.TaskDtos;

namespace Jared.Application.Validators.Task;

public class TaskDetailsDtoValidator : AbstractValidator<TaskDetailsDto>
{
    public TaskDetailsDtoValidator(TaskRootDtoValidator taskRootDoValidator)
    {
        Include(taskRootDoValidator);
        RuleFor(x => x.Description).MaximumLength(500);
    }
}
