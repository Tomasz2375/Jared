using FluentValidation;
using Jared.Dtos.Tasks;

namespace Jared.Validators.Task;

public class TaskDetailsDtoValidator : AbstractValidator<TaskDetailsDto>
{
    public TaskDetailsDtoValidator()
    {
        Include(new TaskRootDtoValidator());

        RuleFor(x => x.Description).MaximumLength(500);
        RuleFor(x => x.ParentId).Must(x => !x.HasValue || x > 0);
    }
}
