using Jared.Shared.Validators.Task;
using Jared.Shared.Validators.User;
using Microsoft.Extensions.DependencyInjection;

namespace Jared.Shared;

public static class DepedencyInjection
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        services.AddScoped<TaskRootDtoValidator>();
        services.AddScoped<TaskDetailsDtoValidator>();
        services.AddScoped<UserLoginDtoValidator>();
        services.AddScoped<UserRegisterDtoValidator>();
        services.AddScoped<UserPasswordDtoValidator>();

        return services;
    }
}
