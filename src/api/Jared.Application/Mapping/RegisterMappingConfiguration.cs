using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Jared.Application.Mapping;

public static class RegisterMappingConfiguration
{
    public static IServiceCollection RegisterMappingConfigurations(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, Mapper>();

        return services;
    }
}
