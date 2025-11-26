using Jared.Application.Handlers.Epics;
using Jared.Application.Handlers.Projects;
using Jared.Application.Handlers.Roles;
using Jared.Application.Handlers.Tasks;
using Jared.Application.Handlers.Users;
using Jared.Application.Handlers.WorkLogs;
using Jared.Application.Services.Filters;
using Jared.Application.Services.TaskHistory;
using Jared.Application.Services.User;
using Jared.Contracts.Epics;
using Jared.Contracts.Projects;
using Jared.Contracts.Roles;
using Jared.Contracts.Tasks;
using Jared.Contracts.Users;
using Jared.Contracts.Worklogs;
using Jared.Domain.Models;
using Jared.Dtos.Epics;
using Jared.Dtos.Projects;
using Jared.Dtos.Roles;
using Jared.Dtos.Tasks;
using Jared.Dtos.Users;
using Jared.Dtos.WorkLogs;
using Jared.Shared.Abstractions;
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
