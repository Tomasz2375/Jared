using FluentValidation;
using Jared.Shared.Dtos.EpicDtos;

namespace Jared.Shared.Validators.Epic;

public class EpicRootDtoValidator : AbstractValidator<EpicRootDto>
{
    public EpicRootDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.ProjectId).NotEmpty();
        RuleFor(x => x.Status).IsInEnum();
        RuleFor(x => x.ParentId).Must(x => !x.HasValue || x > 0);
    }
}
