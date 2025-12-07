using FluentValidation;
using Jared.Dtos.Epics;
using Jared.Dtos.Projects;
using Jared.Dtos.Tasks;
using Jared.Dtos.Users;
using Jared.Dtos.WorkLogs;
using Jared.Validators.Epic;
using Jared.Validators.Project;
using Jared.Validators.Task;
using Jared.Validators.User;
using Jared.Validators.WorkLog;
using Microsoft.Extensions.DependencyInjection;

namespace Jared.Validators;

public static class DependencyInjection
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<EpicDetailsDto>, EpicDetailsDtoValidator>();
        services.AddScoped<IValidator<TaskDetailsDto>, TaskDetailsDtoValidator>();
        services.AddScoped<IValidator<UserRegisterDto>, UserRegisterDtoValidator>();
        services.AddScoped<IValidator<UserLoginDto>, UserLoginDtoValidator>();
        services.AddScoped<IValidator<UserPasswordDto>, UserPasswordDtoValidator>();
        services.AddScoped<IValidator<WorkLogAddDto>, WorkLogAddDtoValidator>();
        services.AddScoped<IValidator<ProjectRootDto>, ProjectRootDtoValidator>();

        return services;
    }
}
