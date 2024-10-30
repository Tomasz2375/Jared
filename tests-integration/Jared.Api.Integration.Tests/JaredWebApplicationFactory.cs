using Jared.Infrastructure.Persistence;
using Jared.Shared.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Data.Common;
using Testcontainers.MsSql;

namespace Jared.Api.Integration.Tests;

public class JaredWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer msSqlContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2019-latest")
        .Build();

    private DbConnection dbConnection = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<DataContext>));
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(dbConnection);
            });
        });
    }

    public async Task InitializeAsync()
    {
        await msSqlContainer.StartAsync();
        dbConnection = new SqlConnection(msSqlContainer.GetConnectionString());
        await seedDatabaseAsync();
    }

    public new async Task DisposeAsync()
    {
        await msSqlContainer.StopAsync();
    }

    private async Task seedDatabaseAsync()
    {
        using var scope = Services.CreateScope();
        var dataContext = scope.ServiceProvider.GetRequiredService<IDataContext>();
        await dataContext.Database.MigrateAsync();
        dataContext.Database.EnsureCreated();
        await dataContext.Seed();
    }
}
