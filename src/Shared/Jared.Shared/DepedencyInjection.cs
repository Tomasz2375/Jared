using FluentValidation;
using Jared.Shared.Dtos.TaskDtos;
using Jared.Shared.Dtos.UserDtos;
using Jared.Shared.Validators.Task;
using Jared.Shared.Validators.User;
using Microsoft.Extensions.DependencyInjection;

namespace Jared.Shared;

public static class DepedencyInjection
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        services.AddScoped<IValidator<TaskDetailsDto>, TaskDetailsDtoValidator>();
        services.AddScoped<IValidator<UserRegisterDto>, UserRegisterDtoValidator>();
        services.AddScoped<IValidator<UserLoginDto>, UserLoginDtoValidator>();
        services.AddScoped<IValidator<UserPasswordDto>, UserPasswordDtoValidator>();

        return services;
    }
}
