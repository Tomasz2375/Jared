using FluentValidation;
using Jared.Application.Dtos.UserDtos;

namespace Jared.Application.Validators.User;

public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(50);
    }
}
