using Jared.Domain.Models;
using Jared.Infrastructure.Persistence;
using Jared.Shared.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jared.Api.Integration.Tests;

public class JaredWebApplicationFactory : WebApplicationFactory<Program>, IDisposable
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var dbContextOptions = services.SingleOrDefault(s =>
                s.ServiceType == typeof(DbContextOptions<DataContext>));

            if (dbContextOptions is not null)
            {
                services.Remove(dbContextOptions);
            }
        });

        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddJsonFile("appsettings.Tests.json");
        });

        builder.ConfigureServices(async (context, services) =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<DataContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            var connectionString = context
                .Configuration.GetConnectionString("JaredConnectionString");

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped(client => new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7050/api/")
            });


            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var dataContext = scope.ServiceProvider.GetRequiredService<IDataContext>();
            dataContext.Database.EnsureDeleted();
            dataContext.Database.Migrate();
            dataContext.Database.EnsureCreated();

            await dataContext.Seed();
        });
    }

    public new void Dispose()
    {
        using var scope = Services.CreateScope();
        var dataContext = scope.ServiceProvider.GetRequiredService<IDataContext>();
        dataContext.Database.EnsureDeleted();
        base.Dispose();
    }
}
