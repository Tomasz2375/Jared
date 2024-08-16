using FluentAssertions;
using System.Reflection;

namespace Jared.Architecture.Tests;

public class ArchitectureTests
{
    #region Const
    private const string SHARED_NAMESPACE = "Jared.Shared";
    private const string DOMAIN_NAMESPACE = "Jared.Domain";
    private const string APPLICATION_NAMESPACE = "Jared.Application";
    private const string INFRASTRUCTURE_NAMESPACE = "Jared.Infrastructure";
    private const string PRESENTATION_NAMESPACE = "Jared.Presentation";
    private const string API_NAMESPACE = "Jared.Api";
    private const string APP_NAMESPACE = "Jared.App";
    #endregion

    #region API
    [Fact]
    public void Shared_ShouldNotHaveDependencyOnOtherProject()
    {
        // Arrange
        var assembly = Assembly.Load(SHARED_NAMESPACE);
        var assemblyName = assembly.GetReferencedAssemblies();

        var otherProjects = new[]
        {
            DOMAIN_NAMESPACE,
            APPLICATION_NAMESPACE,
            INFRASTRUCTURE_NAMESPACE,
            PRESENTATION_NAMESPACE,
            API_NAMESPACE,
            APP_NAMESPACE,
        };

        // Act
        var result = otherProjects.Any(x => assemblyName.Select(y => y.Name).Contains(x));

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Domain_ShouldNotHaveDependencyOnOtherProject()
    {
        // Arrange
        var assembly = Assembly.Load(DOMAIN_NAMESPACE);
        var assemblyName = assembly.GetReferencedAssemblies();

        var otherProjects = new[]
        {
            APPLICATION_NAMESPACE,
            INFRASTRUCTURE_NAMESPACE,
            PRESENTATION_NAMESPACE,
            API_NAMESPACE,
            APP_NAMESPACE,
        };

        // Act
        var result = otherProjects.Any(x => assemblyName.Select(y => y.Name).Contains(x));

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Application_ShouldNotHaveDependencyOnOtherProject()
    {
        // Arrange
        var assembly = Assembly.Load(APPLICATION_NAMESPACE);
        var assemblyName = assembly.GetReferencedAssemblies();

        var otherProjects = new[]
        {
            INFRASTRUCTURE_NAMESPACE,
            PRESENTATION_NAMESPACE,
            API_NAMESPACE,
            APP_NAMESPACE,
        };

        // Act
        var result = otherProjects.Any(x => assemblyName.Select(y => y.Name).Contains(x));

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Infrastructure_ShouldNotHaveDependencyOnOtherProject()
    {
        // Arrange
        var assembly = Assembly.Load(INFRASTRUCTURE_NAMESPACE);
        var assemblyName = assembly.GetReferencedAssemblies();

        var otherProjects = new[]
        {
            PRESENTATION_NAMESPACE,
            API_NAMESPACE,
            APP_NAMESPACE,
        };

        // Act
        var result = otherProjects.Any(x => assemblyName.Select(y => y.Name).Contains(x));

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Api_ShouldNotHaveDependencyOnOtherProject()
    {
        // Arrange
        var assembly = Assembly.Load(API_NAMESPACE);
        var assemblyName = assembly.GetReferencedAssemblies();

        var otherProjects = new[]
        {
            PRESENTATION_NAMESPACE,
            APP_NAMESPACE,
        };

        // Act
        var result = otherProjects.Any(x => assemblyName.Select(y => y.Name).Contains(x));

        // Assert
        result.Should().BeFalse();
    }
    #endregion

    #region APP
    [Fact]
    public void Presentation_ShouldNotHaveDependencyOnOtherProject()
    {
        // Arrange
        var assembly = Assembly.Load(PRESENTATION_NAMESPACE);
        var assemblyName = assembly.GetReferencedAssemblies();

        var otherProjects = new[]
        {
            DOMAIN_NAMESPACE,
            APPLICATION_NAMESPACE,
            INFRASTRUCTURE_NAMESPACE,
            API_NAMESPACE,
            APP_NAMESPACE,
        };

        // Act
        var result = otherProjects.Any(x => assemblyName.Select(y => y.Name).Contains(x));

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void App_ShouldNotHaveDependencyOnOtherProject()
    {
        // Arrange
        var assembly = Assembly.Load(PRESENTATION_NAMESPACE);
        var assemblyName = assembly.GetReferencedAssemblies();

        var otherProjects = new[]
        {
            DOMAIN_NAMESPACE,
            APPLICATION_NAMESPACE,
            INFRASTRUCTURE_NAMESPACE,
            API_NAMESPACE,
        };

        // Act
        var result = otherProjects.Any(x => assemblyName.Select(y => y.Name).Contains(x));

        // Assert
        result.Should().BeFalse();
    }
    #endregion
}
