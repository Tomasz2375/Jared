using Jared.Infrastructure.Persistence;
using Jared.Shared.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jared.Api.Integration.Tests;

public class JaredWebApplicationFactory : WebApplicationFactory<Program>, IDisposable
{
    private const string ENVIRONMENT = "Tests";
    private string dbConnection = 
        $"Server={Environment.GetEnvironmentVariable("JARED_DB_SERVER")!};Integrated Security=True;";
    private string dbName = Environment.GetEnvironmentVariable("JARED_TEST_DB_NAME")!;
    private string snapshotName = Environment.GetEnvironmentVariable("JARED_SNAPSHOT_NAME")!;
    private string snapshotPath = Environment.GetEnvironmentVariable("JARED_SNAPSHOT_PATH")!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(ENVIRONMENT);

        builder.ConfigureTestServices(async services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<DataContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            var connectionString = services
                .BuildServiceProvider()
                .GetRequiredService<IConfiguration>()
                .GetConnectionString("JaredConnectionString");

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var dataContext = scope.ServiceProvider.GetRequiredService<IDataContext>();
            DropSnapshot();
            dataContext.Database.EnsureDeleted();
            dataContext.Database.Migrate();
            dataContext.Database.EnsureCreated();

            await dataContext.Seed();

            CreateSnapshot(dataContext);
        });
    }

    public new void Dispose()
    {
        using var scope = Services.CreateScope();
        var dataContext = scope.ServiceProvider.GetRequiredService<IDataContext>();
        DropSnapshot();
        dataContext.Database.EnsureDeleted();
        base.Dispose();
    }

    public void CreateSnapshot(IDataContext dataContext)
    {
        var sql =
            $@"CREATE DATABASE {snapshotName} ON 
            (NAME = {dbName}, FILENAME = '{snapshotPath}')
            AS SNAPSHOT OF {dbName}";

        using SqlConnection connection = new(dbConnection);
        connection.Open();
        using SqlCommand command = new(sql, connection);
        command.ExecuteNonQuery();
    }

    public void RestoreSnapshot()
    {
        var sql =
            $"USE master; " +
            $"ALTER DATABASE Jared_Tests SET SINGLE_USER WITH ROLLBACK IMMEDIATE; " +
            $"RESTORE DATABASE {dbName} FROM DATABASE_SNAPSHOT = '{snapshotName}' " +
            $"ALTER DATABASE Jared_Tests SET MULTI_USER; ";

        using SqlConnection connection = new(dbConnection);
        connection.Open();
        using SqlCommand command = new(sql, connection);
        command.ExecuteNonQuery();
    }

    public void DropSnapshot()
    {
        var sql = 
            @$"IF EXISTS (SELECT * FROM sys.databases WHERE name = '{snapshotName}')
            BEGIN
            DROP DATABASE {snapshotName}
            END";

        using SqlConnection connection = new(dbConnection);
        connection.Open();
        using SqlCommand command = new(sql, connection);
        command.ExecuteNonQuery();
    }
}
