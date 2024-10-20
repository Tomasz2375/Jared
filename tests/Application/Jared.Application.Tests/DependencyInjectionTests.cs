using Jared.Application.Requests.Epics.Create;
using Jared.Application.Requests.Epics.Details;
using Jared.Application.Requests.Epics.List;
using Jared.Application.Requests.Epics.Page;
using Jared.Application.Requests.Epics.Update;
using Jared.Application.Requests.Projects.Create;
using Jared.Application.Requests.Projects.Details;
using Jared.Application.Requests.Projects.List;
using Jared.Application.Requests.Projects.Page;
using Jared.Application.Requests.Projects.Update;
using Jared.Application.Requests.Tasks.Create;
using Jared.Application.Requests.Tasks.Details;
using Jared.Application.Requests.Tasks.List;
using Jared.Application.Requests.Tasks.Page;
using Jared.Application.Requests.Tasks.Update;
using Jared.Application.Requests.Users.List;
using Jared.Application.Requests.Users.Login;
using Jared.Application.Requests.Users.Password;
using Jared.Application.Requests.Users.Register;
using Jared.Application.Requests.Users.Update;
using Jared.Application.Services.Filters;
using Jared.Application.Services.TaskHistory;
using Jared.Application.Services.User;
using Jared.Domain.Models;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.EpicDtos;
using Jared.Shared.Dtos.ProjectDtos;
using Jared.Shared.Dtos.TaskDtos;
using Jared.Shared.Dtos.UserDtos;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Task = Jared.Domain.Models.Task;

namespace Jared.Application.Tests;

public class DependencyInjectionTests
{
    [Fact]
    public void AddApplication_ShouldRegisterAppropriateServicesCount()
    {
        // Arrange
        ServiceCollection services = new();

        // Act
        DependencyInjection.AddApplication(services);

        // Assert
        Assert.Equal(33, services.Count);
    }

    [Fact]
    public void AddApplication_ShouldAddExpectedHandlers()
    {
        // Arrange
        ServiceCollection services = new();

        // Act
        DependencyInjection.AddApplication(services);

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
    public void AddApplication_ShouldAddExpectedServices()
    {
        // Arrange
        ServiceCollection services = new();

        // Act
        DependencyInjection.AddApplication(services);

        // Assert
        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(ITaskHistoryService) &&
            x.ImplementationType == typeof(TaskHistoryService) &&
            x.Lifetime == ServiceLifetime.Scoped));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IUserService) &&
            x.ImplementationType == typeof(UserService) &&
            x.Lifetime == ServiceLifetime.Scoped));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IFilterStrategy<Task>) &&
            x.ImplementationType == typeof(TaskFilter) &&
            x.Lifetime == ServiceLifetime.Scoped));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IFilterStrategy<Epic>) &&
            x.ImplementationType == typeof(EpicFilter) &&
            x.Lifetime == ServiceLifetime.Scoped));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IFilterStrategy<Project>) &&
            x.ImplementationType == typeof(ProjectFilter) &&
            x.Lifetime == ServiceLifetime.Scoped));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IFilterBuilder<Task>) &&
            x.ImplementationType == typeof(FilterBuilder<Task>) &&
            x.Lifetime == ServiceLifetime.Scoped));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IFilterBuilder<Epic>) &&
            x.ImplementationType == typeof(FilterBuilder<Epic>) &&
            x.Lifetime == ServiceLifetime.Scoped));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IFilterBuilder<Project>) &&
            x.ImplementationType == typeof(FilterBuilder<Project>) &&
            x.Lifetime == ServiceLifetime.Scoped));
    }
}
