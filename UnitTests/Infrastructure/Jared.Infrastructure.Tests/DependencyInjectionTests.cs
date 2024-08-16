using Jared.Infrastructure.Persistence;
using Jared.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Jared.Infrastructure.Tests;

public class DependencyInjectionTests
{
    [Fact]
    public void AddInfrastructure_ShouldRegisterAppropriateServicesCount()
    {
        // Arrange
        Mock<IConfiguration> configurationMock = new();
        ServiceCollection services = new();
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", string.Empty);

        // Act
        DependencyInjection.AddInfrastructure(services, configurationMock.Object);

        // Assert
        Assert.Single(services);
    }

    [Fact]
    public void AddInfrastructure_ShouldAddExpectedServices()
    {
        // Arrange
        Mock<IConfiguration> configurationMock = new();
        ServiceCollection services = new();
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", string.Empty);

        // Act
        DependencyInjection.AddInfrastructure(services, configurationMock.Object);

        // Assert
        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IDataContext) &&
            x.ImplementationType == typeof(DataContext) &&
            x.Lifetime == ServiceLifetime.Transient));
    }

    [Theory]
    [InlineData("Development")]
    [InlineData("Production")]
    public void AddInfrastructure_WhenEnvironmentIsSet_ShouldRegisterAppropriateServicesCount(string environmentName)
    {
        // Arrange
        Mock<IConfiguration> configurationMock = new();
        ServiceCollection services = new();
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", environmentName);
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        // Act
        DependencyInjection.AddInfrastructure(services, configurationMock.Object);

        // Assert
        Assert.Equal(4, services.Count);
        Assert.Equal(environmentName, environment);
    }
}
