using System.Reflection;
using Blazored.LocalStorage;
using Jared.Client.Abstractions;
using Jared.Client.Services;
using Jared.Shared.Behaviours;
using Microsoft.Extensions.DependencyInjection;
using NotificationService = Jared.Client.Services.NotificationService;

namespace Jared.Client;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(assembly);
            config.AddOpenBehavior(typeof(RequestLogginPipelineBehaviour<,>));
        });
        services.AddBlazoredLocalStorage();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IDialogService, DialogService>();
        return services;
    }
}
