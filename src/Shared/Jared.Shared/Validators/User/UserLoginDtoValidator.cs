using FluentValidation;
using Jared.Shared.Dtos.UserDtos;

namespace Jared.Shared.Validators.User;

public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(50);
    }
}
