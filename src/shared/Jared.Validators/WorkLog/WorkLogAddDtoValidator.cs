using FluentValidation;
using Jared.Dtos.WorkLogs;

namespace Jared.Validators.WorkLog;

public class WorkLogAddDtoValidator : AbstractValidator<WorkLogAddDto>
{
    public WorkLogAddDtoValidator()
    {
        RuleFor(x => x.Hours)
            .InclusiveBetween(0, 23);
        RuleFor(x => x.Minutes)
            .InclusiveBetween(0, 59);
        RuleFor(x => x)
            .Custom((dto, context) =>
            {
                if (dto.Hours == 0 && dto.Minutes == 0)
                {
                    context.AddFailure(nameof(dto.Hours), "Time can't be zero.");
                    context.AddFailure(nameof(dto.Minutes), "Time can't be zero.");
                }
            });
        RuleFor(x => x.WorkDate)
            .Must(notFuture).WithMessage("Date can't be in the future.")
            .Must(notOlderThanMonth).WithMessage("Date can't be more than one month old.");
    }

    private static bool notFuture(DateTime? date) =>
        date <= DateTime.Now;

    private static bool notOlderThanMonth(DateTime? date) =>
        date > DateTime.Now.AddMonths(-1);
}
