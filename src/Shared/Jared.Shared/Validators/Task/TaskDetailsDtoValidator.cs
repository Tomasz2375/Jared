using FluentValidation;
using Jared.Shared.Dtos.TaskDtos;

namespace Jared.Shared.Validators.Task;

public class TaskDetailsDtoValidator : AbstractValidator<TaskDetailsDto>
{
    public TaskDetailsDtoValidator(TaskRootDtoValidator taskRootDoValidator)
    {
        Include(taskRootDoValidator);
        RuleFor(x => x.Description).MaximumLength(500);
    }
}
