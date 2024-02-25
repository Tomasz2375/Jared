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
        services.AddDbContext<DataContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("JaredConnectionString")));
        services.AddScoped<IDataContext, DataContext>();

        return services;
    }
}
