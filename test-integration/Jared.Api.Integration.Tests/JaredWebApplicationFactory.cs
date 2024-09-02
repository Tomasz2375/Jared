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
    private const string DB_NAME = "Jared_Tests";
    private const string DB_CONNECTION = "Server=localhost;Integrated Security=True;";
    private const string SNAPSHOT_NAME = "Jared_Tests_Snapshot";
    private const string SNAPSHOT_PATH = "C:\\Program Files\\Microsoft SQL Server\\MSSQL16.MSSQLSERVER\\MSSQL\\DATA\\Jared_Tests_Snapshot.ss";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddJsonFile("appsettings.Tests.json");
        });

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
        string connectionString = DB_CONNECTION;
        var sql =
            $@"CREATE DATABASE {SNAPSHOT_NAME} ON 
            (NAME = {DB_NAME}, FILENAME = '{SNAPSHOT_PATH}')
            AS SNAPSHOT OF {DB_NAME}";

        using SqlConnection connection = new(connectionString);
        connection.Open();
        using SqlCommand command = new(sql, connection);
        command.ExecuteNonQuery();
    }

    public void RestoreSnapshot()
    {
        string connectionString = DB_CONNECTION;
        var sql =
            $"USE master; " +
            $"ALTER DATABASE Jared_Tests SET SINGLE_USER WITH ROLLBACK IMMEDIATE; " +
            $"RESTORE DATABASE {DB_NAME} FROM DATABASE_SNAPSHOT = '{SNAPSHOT_NAME}' " +
            $"ALTER DATABASE Jared_Tests SET MULTI_USER; ";

        using SqlConnection connection = new(connectionString);
        connection.Open();
        using SqlCommand command = new(sql, connection);
        command.ExecuteNonQuery();
    }

    public void DropSnapshot()
    {
        string connectionString = DB_CONNECTION;
        var sql = 
            @$"IF EXISTS (SELECT * FROM sys.databases WHERE name = '{SNAPSHOT_NAME}')
            BEGIN
            DROP DATABASE {SNAPSHOT_NAME}
            END";

        using SqlConnection connection = new(connectionString);
        connection.Open();
        using SqlCommand command = new(sql, connection);
        command.ExecuteNonQuery();
    }
}
