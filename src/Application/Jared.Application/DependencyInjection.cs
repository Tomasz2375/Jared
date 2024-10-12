using Jared.Application.Behaviours;
using Jared.Application.Services.Filters;
using Jared.Application.Services.TaskHistory;
using Jared.Application.Services.User;
using Jared.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Task = Jared.Domain.Models.Task;

namespace Jared.Application;

public static class DependencyInjection 
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(config => config.RegisterServicesFromAssemblies(assembly));
        services.AddScoped<ITaskHistoryService, TaskHistoryService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IFilterStrategy<Task>, TaskFilter>();
        services.AddScoped<IFilterStrategy<Epic>, EpicFilter>();
        services.AddScoped<IFilterStrategy<Project>, ProjectFilter>();
        services.AddScoped<IFilterBuilder<Task>, FilterBuilder<Task>>();
        services.AddScoped<IFilterBuilder<Epic>, FilterBuilder<Epic>>();
        services.AddScoped<IFilterBuilder<Project>, FilterBuilder<Project>>();

        return services;
    }
}
