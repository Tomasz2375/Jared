using FluentValidation;
using Jared.Shared.Dtos.UserDtos;

namespace Jared.Application.Validators.User;

public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
{
    public UserRegisterDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(50);
        RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(y => y.Password).WithMessage("Wartość pola 'Confirm Password' musi być równa 'Password'.");
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.Now.AddYears(-18))
            .WithMessage($"Wartość pola 'Date Of Birth' musi być mniejsza niż '{DateTime.Now.AddYears(-18).ToString("dd.MM.yyyy")}'");
    }
}
