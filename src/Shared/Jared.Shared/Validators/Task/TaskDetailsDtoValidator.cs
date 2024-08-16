﻿using FluentValidation;
using Jared.Shared.Dtos.TaskDtos;

namespace Jared.Shared.Validators.Task;

public class TaskDetailsDtoValidator : AbstractValidator<TaskDetailsDto>
{
    public TaskDetailsDtoValidator()
    {
        Include(new TaskRootDtoValidator());

        RuleFor(x => x.Description).MaximumLength(500);
    }
}
