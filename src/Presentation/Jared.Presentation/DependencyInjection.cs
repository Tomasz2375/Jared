using Blazored.LocalStorage;
using Jared.Presentation.Services;
using Jared.Shared.Behaviours;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Jared.Presentation;

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

        return services;
    }
}
