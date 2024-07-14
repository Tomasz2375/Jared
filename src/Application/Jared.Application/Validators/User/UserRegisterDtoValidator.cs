using FluentValidation;
using Jared.Application.Dtos.UserDtos;
using Jared.Domain.Interfaces;
using UserEntity = Jared.Domain.Models.User;

namespace Jared.Application.Validators.User;

public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
{
    public UserRegisterDtoValidator(
        IDataContext dataContext)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .Custom((value, context) =>
            {
                var emailInUse = dataContext.Set<UserEntity>().Any(x => x.Email == value);

                if (emailInUse)
                {
                    context.AddFailure("Email", "That email is taken");
                }
            });
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(50);
        RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(y => y.Password).WithMessage("Wartość pola 'Confirm Password' musi być równa 'Password'.");
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.Now.AddYears(-18))
            .WithMessage($"Wartość pola 'Date Of Birth' musi być mniejsza niż '{DateTime.Now.AddYears(-18).ToString("dd.MM.yyyy")}'");
    }
}
