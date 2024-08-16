using Blazored.LocalStorage;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Jared.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(assembly);
        services.AddBlazoredLocalStorage();

        return services;
    }
}
