using FluentValidation;
using Jared.Shared.Dtos.TaskDtos;
using Jared.Shared.Dtos.UserDtos;
using Jared.Shared.Validators.Task;
using Jared.Shared.Validators.User;
using Microsoft.Extensions.DependencyInjection;

namespace Jared.Shared.Tests;

public class DependencyInjectionTests
{

    [Fact]
    public void AddShared_ShouldRegisterAppropriateServicesCount()
    {
        // Arrange
        ServiceCollection services = new();

        // Act
        DependencyInjection.AddShared(services);

        // Assert
        Assert.Equal(4, services.Count);
    }

    [Fact]
    public void AddShared_ShouldAddExpectedValidators()
    {
        // Arrange
        ServiceCollection services = new();

        // Act
        DependencyInjection.AddShared(services);

        // Assert
        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IValidator<TaskDetailsDto>) &&
            x.ImplementationType == typeof(TaskDetailsDtoValidator) &&
            x.Lifetime == ServiceLifetime.Scoped));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IValidator<UserRegisterDto>) &&
            x.ImplementationType == typeof(UserRegisterDtoValidator) &&
            x.Lifetime == ServiceLifetime.Scoped));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IValidator<UserLoginDto>) &&
            x.ImplementationType == typeof(UserLoginDtoValidator) &&
            x.Lifetime == ServiceLifetime.Scoped));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IValidator<UserPasswordDto>) &&
            x.ImplementationType == typeof(UserPasswordDtoValidator) &&
            x.Lifetime == ServiceLifetime.Scoped));
    }
}
