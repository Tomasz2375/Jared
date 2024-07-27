using Jared.Domain.Interfaces;
using Jared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jared.Infrastructure;

public static class DepedencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        switch (environment)
        {
            case "Development":
                services.AddDbContext<DataContext>(options => options.UseSqlServer(
                    configuration.GetConnectionString("JaredDevelopmentConnectionString")));
                break;
            case "Production":
                services.AddDbContext<DataContext>(options => options.UseSqlServer(
                    configuration.GetConnectionString("JaredConnectionString")));
                break;
        }

        services.AddTransient<IDataContext, DataContext>();

        return services;
    }
}
