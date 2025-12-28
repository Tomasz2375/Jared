using FluentValidation;
using Jared.Dtos.Auth;
using Jared.Dtos.Projects;
using Jared.Dtos.Tasks;
using Jared.Dtos.Users;
using Jared.Dtos.WorkLogs;
using Jared.Validators.Project;
using Jared.Validators.Task;
using Jared.Validators.User;
using Jared.Validators.WorkLog;
using Microsoft.Extensions.DependencyInjection;

namespace Jared.Validators.Tests;

public class DependencyInjectionTests
{
    [Fact]
    public void AddValidators_ShouldRegisterAppropriateServicesCount()
    {
        // Arrange
        ServiceCollection services = new();

        // Act
        DependencyInjection.AddValidators(services);

        // Assert
        Assert.Equal(6, services.Count);
    }

    [Fact]
    public void AddValidators_ShouldAddExpectedValidators()
    {
        // Arrange
        ServiceCollection services = new();

        // Act
        DependencyInjection.AddValidators(services);

        // Assert
        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IValidator<TaskDetailsDto>) &&
            x.ImplementationType == typeof(TaskDetailsDtoValidator) &&
            x.Lifetime == ServiceLifetime.Scoped));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IValidator<RegisterRequestDto>) &&
            x.ImplementationType == typeof(UserRegisterDtoValidator) &&
            x.Lifetime == ServiceLifetime.Scoped));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IValidator<LoginRequestDto>) &&
            x.ImplementationType == typeof(UserLoginDtoValidator) &&
            x.Lifetime == ServiceLifetime.Scoped));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IValidator<UserPasswordDto>) &&
            x.ImplementationType == typeof(UserPasswordDtoValidator) &&
            x.Lifetime == ServiceLifetime.Scoped));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IValidator<WorkLogAddDto>) &&
            x.ImplementationType == typeof(WorkLogAddDtoValidator) &&
            x.Lifetime == ServiceLifetime.Scoped));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IValidator<ProjectRootDto>) &&
            x.ImplementationType == typeof(ProjectRootDtoValidator) &&
            x.Lifetime == ServiceLifetime.Scoped));
    }
}
