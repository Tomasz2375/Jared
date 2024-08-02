using Jared.Application.Services.TaskHistory;
using Jared.Application.Services.User;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Jared.Application;

public static class DepedencyInjection 
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(assembly);
        services.AddScoped<ITaskHistoryService, TaskHistoryService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
