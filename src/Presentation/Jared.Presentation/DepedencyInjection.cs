using Microsoft.Extensions.DependencyInjection;

namespace Jared.Presentation;

public static class DepedencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        return services;
    }
}
