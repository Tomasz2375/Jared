using System.Reflection;

namespace Jared.Architecture.Tests;

public class ArchitectureTests
{
    #region const
    private const string SHARED_NAMESPACE = "Jared.Shared";
    private const string DTOS_NAMESPACE = "Jared.Dtos";
    private const string VALIDATORS_NAMESPACE = "Jared.Validators";
    private const string DOMAIN_NAMESPACE = "Jared.Domain";
    private const string APPLICATION_NAMESPACE = "Jared.Application";
    private const string INFRASTRUCTURE_NAMESPACE = "Jared.Infrastructure";
    private const string PRESENTATION_NAMESPACE = "Jared.Presentation";
    private const string CLIENT_NAMESPACE = "Jared.Client";
    private const string UI_NAMESPACE = "Jared.UI";
    private const string API_NAMESPACE = "Jared.Api";
    private const string APP_NAMESPACE = "Jared.App";
    #endregion

    #region shared
    [Fact]
    public void Shared_ShouldNotHaveDependencyOnOtherProject()
    {
        // Arrange
        var assembly = Assembly.Load(SHARED_NAMESPACE);
        var assemblyName = assembly.GetReferencedAssemblies();

        var otherProjects = new[]
        {
            DTOS_NAMESPACE,
            VALIDATORS_NAMESPACE,
            DOMAIN_NAMESPACE,
            APPLICATION_NAMESPACE,
            INFRASTRUCTURE_NAMESPACE,
            PRESENTATION_NAMESPACE,
            CLIENT_NAMESPACE,
            UI_NAMESPACE,
            API_NAMESPACE,
            APP_NAMESPACE,
        };

        // Act
        var result = otherProjects.Any(x => assemblyName.Select(y => y.Name).Contains(x));

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Dtos_ShouldNotHaveDependencyOnOtherProject()
    {
        // Arrange
        var assembly = Assembly.Load(DTOS_NAMESPACE);
        var assemblyName = assembly.GetReferencedAssemblies();

        var otherProjects = new[]
        {
            VALIDATORS_NAMESPACE,
            DOMAIN_NAMESPACE,
            APPLICATION_NAMESPACE,
            INFRASTRUCTURE_NAMESPACE,
            PRESENTATION_NAMESPACE,
            CLIENT_NAMESPACE,
            UI_NAMESPACE,
            API_NAMESPACE,
            APP_NAMESPACE,
        };

        // Act
        var result = otherProjects.Any(x => assemblyName.Select(y => y.Name).Contains(x));

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Validators_ShouldNotHaveDependencyOnOtherProject()
    {
        // Arrange
        var assembly = Assembly.Load(VALIDATORS_NAMESPACE);
        var assemblyName = assembly.GetReferencedAssemblies();

        var otherProjects = new[]
        {
            DOMAIN_NAMESPACE,
            APPLICATION_NAMESPACE,
            INFRASTRUCTURE_NAMESPACE,
            PRESENTATION_NAMESPACE,
            CLIENT_NAMESPACE,
            UI_NAMESPACE,
            API_NAMESPACE,
            APP_NAMESPACE,
        };

        // Act
        var result = otherProjects.Any(x => assemblyName.Select(y => y.Name).Contains(x));

        // Assert
        Assert.False(result);
    }
    #endregion

    #region API
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
            CLIENT_NAMESPACE,
            UI_NAMESPACE,
            API_NAMESPACE,
            APP_NAMESPACE,
        };

        // Act
        var result = otherProjects.Any(x => assemblyName.Select(y => y.Name).Contains(x));

        // Assert
        Assert.False(result);
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
            CLIENT_NAMESPACE,
            UI_NAMESPACE,
            API_NAMESPACE,
            APP_NAMESPACE,
        };

        // Act
        var result = otherProjects.Any(x => assemblyName.Select(y => y.Name).Contains(x));

        // Assert
        Assert.False(result);
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
            CLIENT_NAMESPACE,
            UI_NAMESPACE,
            API_NAMESPACE,
            APP_NAMESPACE,
        };

        // Act
        var result = otherProjects.Any(x => assemblyName.Select(y => y.Name).Contains(x));

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Presentation_ShouldNotHaveDependencyOnOtherProject()
    {
        // Arrange
        var assembly = Assembly.Load(PRESENTATION_NAMESPACE);
        var assemblyName = assembly.GetReferencedAssemblies();

        var otherProjects = new[]
        {
            INFRASTRUCTURE_NAMESPACE,
            CLIENT_NAMESPACE,
            UI_NAMESPACE,
            API_NAMESPACE,
            APP_NAMESPACE,
        };

        // Act
        var result = otherProjects.Any(x => assemblyName.Select(y => y.Name).Contains(x));

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Api_ShouldNotHaveDependencyOnOtherProject()
    {
        // Arrange
        var assembly = Assembly.Load(API_NAMESPACE);
        var assemblyName = assembly.GetReferencedAssemblies();

        var otherProjects = new[]
        {
            CLIENT_NAMESPACE,
            UI_NAMESPACE,
            APP_NAMESPACE,
        };

        // Act
        var result = otherProjects.Any(x => assemblyName.Select(y => y.Name).Contains(x));

        // Assert
        Assert.False(result);
    }
    #endregion

    #region APP

    [Fact]
    public void Client_ShouldNotHaveDependencyOnOtherProject()
    {
        // Arrange
        var assembly = Assembly.Load(CLIENT_NAMESPACE);
        var assemblyName = assembly.GetReferencedAssemblies();

        var otherProjects = new[]
        {
            DOMAIN_NAMESPACE,
            APPLICATION_NAMESPACE,
            INFRASTRUCTURE_NAMESPACE,
            PRESENTATION_NAMESPACE,
            UI_NAMESPACE,
            API_NAMESPACE,
            APP_NAMESPACE,
        };

        // Act
        var result = otherProjects.Any(x => assemblyName.Select(y => y.Name).Contains(x));

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void UI_ShouldNotHaveDependencyOnOtherProject()
    {
        // Arrange
        var assembly = Assembly.Load(UI_NAMESPACE);
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
        Assert.False(result);
    }

    [Fact]
    public void App_ShouldNotHaveDependencyOnOtherProject()
    {
        // Arrange
        var assembly = Assembly.Load(APP_NAMESPACE);
        var assemblyName = assembly.GetReferencedAssemblies();

        var otherProjects = new[]
        {
            DOMAIN_NAMESPACE,
            APPLICATION_NAMESPACE,
            INFRASTRUCTURE_NAMESPACE,
            PRESENTATION_NAMESPACE,
            API_NAMESPACE,
        };

        // Act
        var result = otherProjects.Any(x => assemblyName.Select(y => y.Name).Contains(x));

        // Assert
        Assert.False(result);
    }
    #endregion
}
