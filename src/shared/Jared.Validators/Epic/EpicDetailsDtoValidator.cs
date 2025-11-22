using FluentValidation;
using Jared.Dtos.Epics;

namespace Jared.Validators.Epic;

public class EpicDetailsDtoValidator : AbstractValidator<EpicDetailsDto>
{
    public EpicDetailsDtoValidator()
    {
        Include(new EpicRootDtoValidator());
    }
}
