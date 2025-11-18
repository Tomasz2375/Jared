using Blazored.LocalStorage;
using Jared.Client.Components.Abstractions;
using Jared.Client.Requests.Epics.Create;
using Jared.Client.Requests.Epics.Details;
using Jared.Client.Requests.Epics.List;
using Jared.Client.Requests.Epics.Page;
using Jared.Client.Requests.Epics.Update;
using Jared.Client.Requests.Projects.Create;
using Jared.Client.Requests.Projects.Details;
using Jared.Client.Requests.Projects.List;
using Jared.Client.Requests.Projects.Page;
using Jared.Client.Requests.Projects.Update;
using Jared.Client.Requests.Roles.List;
using Jared.Client.Requests.Tasks.Create;
using Jared.Client.Requests.Tasks.Details;
using Jared.Client.Requests.Tasks.List;
using Jared.Client.Requests.Tasks.Page;
using Jared.Client.Requests.Tasks.Update;
using Jared.Client.Requests.User.List;
using Jared.Client.Requests.User.Login;
using Jared.Client.Requests.User.Password;
using Jared.Client.Requests.User.Register;
using Jared.Client.Requests.User.Update;
using Jared.Client.Requests.User.UpdateRole;
using Jared.Client.Requests.WorkLogs.Statistics;
using Jared.Client.Services;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.EpicDtos;
using Jared.Shared.Dtos.ProjectDtos;
using Jared.Shared.Dtos.Role;
using Jared.Shared.Dtos.TaskDtos;
using Jared.Shared.Dtos.UserDtos;
using Jared.Shared.Dtos.WorkLogDtos;
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
        DependencyInjection.AddPresentation(services);

        // Assert
        Assert.Equal(41, services.Count);
    }

    [Fact]
    public void AddPresentation_ShouldAddExpectedHandlers()
    {
        // Arrange
        ServiceCollection services = new();

        // Act
        DependencyInjection.AddPresentation(services);

        // Assert
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

        // User
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
        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<UserRoleUpdateCommand, Result<bool>>) &&
            x.ImplementationType == typeof(UserRoleUpdateCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        // Role
        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<RoleListQuery, Result<List<RoleListDto>>>) &&
            x.ImplementationType == typeof(RoleListQueryHandler) &&
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
        DependencyInjection.AddPresentation(services);
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
        DependencyInjection.AddPresentation(services);

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
