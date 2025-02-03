using FluentValidation;
using Jared.Shared.Dtos.EpicDtos;
using Jared.Shared.Dtos.TaskDtos;
using Jared.Shared.Dtos.UserDtos;
using Jared.Shared.Dtos.WorkLogDtos;
using Jared.Shared.Validators.Epic;
using Jared.Shared.Validators.Task;
using Jared.Shared.Validators.User;
using Jared.Shared.Validators.WorkLog;
using Microsoft.Extensions.DependencyInjection;

namespace Jared.Shared;

public static class DependencyInjection
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        services.AddScoped<IValidator<EpicDetailsDto>, EpicDetailsDtoValidator>();
        services.AddScoped<IValidator<TaskDetailsDto>, TaskDetailsDtoValidator>();
        services.AddScoped<IValidator<UserRegisterDto>, UserRegisterDtoValidator>();
        services.AddScoped<IValidator<UserLoginDto>, UserLoginDtoValidator>();
        services.AddScoped<IValidator<UserPasswordDto>, UserPasswordDtoValidator>();
        services.AddScoped<IValidator<WorkLogAddDto>, WorkLogAddDtoValidator>();

        return services;
    }
}
