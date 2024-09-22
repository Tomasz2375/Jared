using FluentValidation;
using Jared.Shared.Dtos.EpicDtos;

namespace Jared.Shared.Validators.Epic;

public class EpicDetailsDtoValidator : AbstractValidator<EpicDetailsDto>
{
    public EpicDetailsDtoValidator()
    {
        Include(new EpicRootDtoValidator());
    }
}
