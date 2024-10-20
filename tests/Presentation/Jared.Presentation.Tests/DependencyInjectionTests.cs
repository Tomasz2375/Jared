using Blazored.LocalStorage;
using Jared.Presentation.Requests.Epics.Create;
using Jared.Presentation.Requests.Epics.Details;
using Jared.Presentation.Requests.Epics.List;
using Jared.Presentation.Requests.Epics.Page;
using Jared.Presentation.Requests.Epics.Update;
using Jared.Presentation.Requests.Projects.Create;
using Jared.Presentation.Requests.Projects.Details;
using Jared.Presentation.Requests.Projects.List;
using Jared.Presentation.Requests.Projects.Page;
using Jared.Presentation.Requests.Projects.Update;
using Jared.Presentation.Requests.Tasks.Create;
using Jared.Presentation.Requests.Tasks.Details;
using Jared.Presentation.Requests.Tasks.List;
using Jared.Presentation.Requests.Tasks.Page;
using Jared.Presentation.Requests.Tasks.Update;
using Jared.Presentation.Requests.User.List;
using Jared.Presentation.Requests.User.Login;
using Jared.Presentation.Requests.User.Password;
using Jared.Presentation.Requests.User.Register;
using Jared.Presentation.Requests.User.Update;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.EpicDtos;
using Jared.Shared.Dtos.ProjectDtos;
using Jared.Shared.Dtos.TaskDtos;
using Jared.Shared.Dtos.UserDtos;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;

namespace Jared.Presentation.Tests;

public class DependencyInjectionTests
{
    [Fact]
    public void AddPresentation_ShouldRegisterAppropriateServicesCount()
    {
        // Arrange
        ServiceCollection services = new();

        // Act
        DependencyInjection.AddPresentation(services);

        // Assert
        Assert.Equal(36, services.Count);
    }

    [Fact]
    public void AddApplication_ShouldAddExpectedHandlers()
    {
        // Arrange
        ServiceCollection services = new();

        // Act
        DependencyInjection.AddPresentation(services);

        // Assert
        #region Task
        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<TaskCreateCommand, Result<bool>>) &&
            x.ImplementationType == typeof(TaskCreateCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<TaskDetailsQuery, Result<TaskDetailsDto>>) &&
            x.ImplementationType == typeof(TaskDetailsQueryHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<TaskListQuery, Result<List<TaskListDto>>>) &&
            x.ImplementationType == typeof(TaskListQueryHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<TaskPageQuery, Result<TaskPageDto>>) &&
            x.ImplementationType == typeof(TaskPageQueryHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<TaskUpdateCommand, Result<bool>>) &&
            x.ImplementationType == typeof(TaskUpdateCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));
        #endregion

        #region Epic
        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<EpicCreateCommand, Result<bool>>) &&
            x.ImplementationType == typeof(EpicCreateCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<EpicDetailsQuery, Result<EpicDetailsDto>>) &&
            x.ImplementationType == typeof(EpicDetailsQueryHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<EpicListQuery, Result<List<EpicListDto>>>) &&
            x.ImplementationType == typeof(EpicListQueryHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<EpicPageQuery, Result<EpicPageDto>>) &&
            x.ImplementationType == typeof(EpicPageQueryHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<EpicUpdateCommand, Result<bool>>) &&
            x.ImplementationType == typeof(EpicUpdateCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));
        #endregion

        #region Project
        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<ProjectCreateCommand, Result<bool>>) &&
            x.ImplementationType == typeof(ProjectCreateCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<ProjectDetailsQuery, Result<ProjectDetailsDto>>) &&
            x.ImplementationType == typeof(ProjectDetailsQueryHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<ProjectListQuery, Result<List<ProjectListDto>>>) &&
            x.ImplementationType == typeof(ProjectListQueryHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<ProjectPageQuery, Result<ProjectPageDto>>) &&
            x.ImplementationType == typeof(ProjectPageQueryHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<ProjectUpdateCommand, Result<bool>>) &&
            x.ImplementationType == typeof(ProjectUpdateCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));
        #endregion

        #region User
        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<UserListQuery, Result<List<UserListDto>>>) &&
            x.ImplementationType == typeof(UserListQueryHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<UserLoginCommand, Result<string>>) &&
            x.ImplementationType == typeof(UserLoginCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<UserPasswordCommand, Result<bool>>) &&
            x.ImplementationType == typeof(UserPasswordCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<UserRegisterCommand, Result<bool>>) &&
            x.ImplementationType == typeof(UserRegisterCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<UserUpdateCommand, Result<bool>>) &&
            x.ImplementationType == typeof(UserUpdateCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));
        #endregion
    }

    [Fact]
    public void AddApplication_ShouldRegisterLocalStorageService()
    {
        // Arrange
        Mock<IJSRuntime> jsRuntimeMock = new();
        ServiceCollection services = new();
        services.AddSingleton(jsRuntimeMock.Object);

        // Act
        DependencyInjection.AddPresentation(services);
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var localStorageService = serviceProvider.GetService<ILocalStorageService>();
        var syncLocalStorageService = serviceProvider.GetService<ISyncLocalStorageService>();
        Assert.NotNull(localStorageService);
        Assert.NotNull(syncLocalStorageService);
    }
}