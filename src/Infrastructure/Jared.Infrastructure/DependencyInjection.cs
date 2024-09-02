using Jared.Infrastructure.Persistence;
using Jared.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jared.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        switch (environment)
        {
            case "Development":
                services.AddDbContext<DataContext>(options => options.UseSqlServer(
                    configuration.GetConnectionString("JaredConnectionString")));
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
