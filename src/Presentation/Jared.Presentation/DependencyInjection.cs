using Blazored.LocalStorage;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Jared.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(config => config.RegisterServicesFromAssemblies(assembly));
        services.AddBlazoredLocalStorage();

        return services;
    }
}
