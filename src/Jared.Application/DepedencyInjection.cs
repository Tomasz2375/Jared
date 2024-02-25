using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Jared.Application;

public static class DepedencyInjection 
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DepedencyInjection).Assembly;

        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(assembly);
        services.AddScoped<IMapper, Mapper>();

        return services;
    }
}
