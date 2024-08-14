using FluentValidation;
using Jared.Shared.Dtos.UserDtos;

namespace Jared.Shared.Validators.User;

public class UserPasswordDtoValidator : AbstractValidator<UserPasswordDto>
{
    public UserPasswordDtoValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty().MinimumLength(6).MaximumLength(50);
        RuleFor(x => x.NewPassword)
            .NotEmpty().MinimumLength(6).MaximumLength(50);
        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().MinimumLength(6).MaximumLength(50).Equal(x => x.NewPassword)
            .WithMessage("Wartość pola 'Confirm Password' musi być równa 'Password'.");
    }
}
