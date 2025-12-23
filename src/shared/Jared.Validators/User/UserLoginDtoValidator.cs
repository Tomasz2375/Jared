using FluentValidation;
using Jared.Dtos.Auth;

namespace Jared.Validators.User;

public class UserLoginDtoValidator : AbstractValidator<LoginRequestDto>
{
    public UserLoginDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(50);
    }
}
