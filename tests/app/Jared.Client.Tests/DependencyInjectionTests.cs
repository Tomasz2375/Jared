using Blazored.LocalStorage;
using Jared.Client.Abstractions;
using Jared.Client.Handlers.Auth;
using Jared.Client.Handlers.Epics;
using Jared.Client.Handlers.Projects;
using Jared.Client.Handlers.Roles;
using Jared.Client.Handlers.Tasks;
using Jared.Client.Handlers.Users;
using Jared.Client.Handlers.WorkLogs;
using Jared.Client.Requests.Tasks;
using Jared.Client.Requests.Users;
using Jared.Client.Services;
using Jared.Contracts.Auth;
using Jared.Contracts.Epics;
using Jared.Contracts.Projects;
using Jared.Contracts.Roles;
using Jared.Contracts.Tasks;
using Jared.Contracts.Users;
using Jared.Contracts.Worklogs;
using Jared.Core.Abstractions;
using Jared.Dtos.Auth;
using Jared.Dtos.Epics;
using Jared.Dtos.Projects;
using Jared.Dtos.Roles;
using Jared.Dtos.Tasks;
using Jared.Dtos.Users;
using Jared.Dtos.WorkLogs;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;

namespace Jared.Client.Tests;

public class DependencyInjectionTests
{
    [Fact]
    public void AddPresentation_ShouldRegisterAppropriateServicesCount()
    {
        // Arrange
        ServiceCollection services = new();

        // Act
        DependencyInjection.AddClient(services);

        // Assert
        Assert.Equal(41, services.Count);
    }

    [Fact]
    public void AddPresentation_ShouldAddExpectedHandlers()
    {
        // Arrange
        ServiceCollection services = new();

        // Act
        DependencyInjection.AddClient(services);

        // Assert
        // Auth
        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<LoginCommand, Result<LoginResponseDto>>) &&
            x.ImplementationType == typeof(LoginCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<LogoutCommand, Result<bool>>) &&
            x.ImplementationType == typeof(LogoutCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<RegisterCommand, Result<bool>>) &&
            x.ImplementationType == typeof(RegisterCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        // Epic
        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<EpicCreateCommand, Result<bool>>) &&
            x.ImplementationType == typeof(EpicCreateCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<EpicDetailsQuery, Result<EpicDetailsDto>>) &&
            x.ImplementationType == typeof(EpicDetailsQueryHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<EpicPageQuery, Result<EpicPageDto>>) &&
            x.ImplementationType == typeof(EpicPageQueryHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<EpicUpdateCommand, Result<bool>>) &&
            x.ImplementationType == typeof(EpicUpdateCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        // Project
        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<ProjectCreateCommand, Result<bool>>) &&
            x.ImplementationType == typeof(ProjectCreateCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<ProjectDetailsQuery, Result<ProjectDetailsDto>>) &&
            x.ImplementationType == typeof(ProjectDetailsQueryHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<ProjectPageQuery, Result<ProjectPageDto>>) &&
            x.ImplementationType == typeof(ProjectPageQueryHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<ProjectUpdateCommand, Result<bool>>) &&
            x.ImplementationType == typeof(ProjectUpdateCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        // Role
        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<RoleListQuery, Result<List<RoleListDto>>>) &&
            x.ImplementationType == typeof(RoleListQueryHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        // Task
        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<TaskCreateCommand, Result<bool>>) &&
            x.ImplementationType == typeof(TaskCreateCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<TaskDetailsQuery, Result<TaskDetailsDto>>) &&
            x.ImplementationType == typeof(TaskDetailsQueryHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<TaskPageQuery, Result<TaskPageDto>>) &&
            x.ImplementationType == typeof(TaskPageQueryHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<TaskUpdateCommand, Result<TaskDetailsDto>>) &&
            x.ImplementationType == typeof(TaskUpdateCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        // User
        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<UserDetailsQuery, Result<UserDetailsDto>>) &&
            x.ImplementationType == typeof(UserDetailsQueryHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<UserListQuery, Result<List<UserListDto>>>) &&
            x.ImplementationType == typeof(UserListQueryHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<UserPasswordCommand, Result<bool>>) &&
            x.ImplementationType == typeof(UserPasswordCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<UserUpdateCommand, Result<bool>>) &&
            x.ImplementationType == typeof(UserUpdateCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<UserRoleUpdateCommand, Result<bool>>) &&
            x.ImplementationType == typeof(UserRoleUpdateCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        // WorkLog
        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<WorkLogStatisticsQuery, Result<List<WorkLogStatisticsDto>>>) &&
            x.ImplementationType == typeof(WorkLogStatisticsQueryHandler) &&
            x.Lifetime == ServiceLifetime.Transient));
    }

    [Fact]
    public void AddPresentation_ShouldRegisterLocalStorageService()
    {
        // Arrange
        Mock<IJSRuntime> jsRuntimeMock = new();
        ServiceCollection services = new();
        services.AddSingleton(jsRuntimeMock.Object);

        // Act
        DependencyInjection.AddClient(services);
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var localStorageService = serviceProvider.GetService<ILocalStorageService>();
        var syncLocalStorageService = serviceProvider.GetService<ISyncLocalStorageService>();
        Assert.NotNull(localStorageService);
        Assert.NotNull(syncLocalStorageService);
    }

    [Fact]
    public void AddPresentation_ShouldRegisterReguiredServices()
    {
        // Arrange
        ServiceCollection services = new();

        // Act
        DependencyInjection.AddClient(services);

        // Assert
        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IUserService) &&
            x.ImplementationType == typeof(UserService) &&
            x.Lifetime == ServiceLifetime.Scoped));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(INotificationService) &&
            x.ImplementationType == typeof(NotificationService) &&
            x.Lifetime == ServiceLifetime.Scoped));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IDialogService) &&
            x.ImplementationType == typeof(DialogService) &&
            x.Lifetime == ServiceLifetime.Scoped));
    }
}
