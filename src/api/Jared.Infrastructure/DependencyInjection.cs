using Jared.Domain.Abstractions;
using Jared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jared.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options => options.UseSqlServer(
            configuration.GetConnectionString("JaredConnectionString")));
        services.AddTransient<IDataContext, DataContext>();

        return services;
    }
}
