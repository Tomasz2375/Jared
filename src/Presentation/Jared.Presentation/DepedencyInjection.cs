using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Jared.Presentation;

public static class DepedencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(assembly);

        return services;
    }
}
