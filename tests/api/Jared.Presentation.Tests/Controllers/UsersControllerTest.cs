using AutoFixture.Xunit2;
using Jared.Contracts.Users;
using Jared.Core.Abstractions;
using Jared.Dtos.Users;
using Jared.Presentation.Controllers;
using MediatR;
using Moq;

namespace Jared.Application.Tests.Controllers;

public class UsersControllerTest
{
    private readonly UsersController controller;
    private readonly Mock<IMediator> mediatorMock = new();

    public UsersControllerTest() => controller = new(mediatorMock.Object);

    #region GetAll
    [Theory]
    [AutoData]
    public async Task GetAll_WnenMediatrReturnsOk_ShouldReturnSuccessResult(List<UserListDto> dtos)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<UserListQuery>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Ok(dtos)));

        // Act
        var result = await controller.GetAll();

        // Assert
        Assert.True(result.Success);
        Assert.Equal(dtos, result.Data);
        Assert.Equal(string.Empty, result.Error);
        mediatorMock.Verify(x => x.Send(new UserListQuery(), default), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task GetAll_WnenMediatrReturnsFail_ShouldReturnFailureResult(string errorMessage)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<UserListQuery>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Fail<List<UserListDto>>(errorMessage)));

        // Act
        var result = await controller.GetAll();

        // Assert
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal(errorMessage, result.Error);
        mediatorMock.Verify(x => x.Send(new UserListQuery(), default), Times.Once);
    }
    #endregion

    #region UpdatePassword
    [Theory]
    [AutoData]
    public async Task UpdatePassword_WnenMediatrReturnsOk_ShouldReturnSuccessResult(UserPasswordDto dto)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<UserPasswordCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Ok(true)));

        // Act
        var result = await controller.UpdatePassword(dto.Id, dto);

        // Assert
        Assert.True(result.Success);
        Assert.True(result.Data);
        Assert.Equal(string.Empty, result.Error);
        mediatorMock.Verify(x => x.Send(new UserPasswordCommand(dto), default), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task UpdatePassword_WnenMediatrReturnsFail_ShouldReturnFailureResult(
        UserPasswordDto dto,
        string errorMessage)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<UserPasswordCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Fail<bool>(errorMessage)));

        // Act
        var result = await controller.UpdatePassword(dto.Id, dto);

        // Assert
        Assert.False(result.Success);
        Assert.False(result.Data);
        Assert.Equal(errorMessage, result.Error);
        mediatorMock.Verify(x => x.Send(new UserPasswordCommand(dto), default), Times.Once);
    }
    #endregion

    #region Update
    [Theory]
    [AutoData]
    public async Task Update_WnenMediatrReturnsOk_ShouldReturnSuccessResult(UserDetailsDto dto)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<UserUpdateCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Ok(true)));

        // Act
        var result = await controller.Update(dto.Id, dto);

        // Assert
        Assert.True(result.Success);
        Assert.True(result.Data);
        Assert.Equal(string.Empty, result.Error);
        mediatorMock.Verify(x => x.Send(new UserUpdateCommand(dto), default), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task Update_WnenMediatrReturnsFail_ShouldReturnFailureResult(
        UserDetailsDto dto,
        string errorMessage)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<UserUpdateCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Fail<bool>(errorMessage)));

        // Act
        var result = await controller.Update(dto.Id, dto);

        // Assert
        Assert.False(result.Success);
        Assert.False(result.Data);
        Assert.Equal(errorMessage, result.Error);
        mediatorMock.Verify(x => x.Send(new UserUpdateCommand(dto), default), Times.Once);
    }
    #endregion

    #region UpdateRole
    [Theory]
    [AutoData]
    public async Task UpdateRole_WnenMediatrReturnsOk_ShouldReturnSuccessResult(UserRoleUpdateDto dto)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<UserRoleUpdateCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Ok(true)));

        // Act
        var result = await controller.UpdateRole(dto.Id, dto);

        // Assert
        Assert.True(result.Success);
        Assert.True(result.Data);
        Assert.Equal(string.Empty, result.Error);
        mediatorMock.Verify(x => x.Send(new UserRoleUpdateCommand(dto), default), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task UpdateRole_WnenMediatrReturnsFail_ShouldReturnFailureResult(
        UserRoleUpdateDto dto,
        string errorMessage)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<UserRoleUpdateCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Fail<bool>(errorMessage)));

        // Act
        var result = await controller.UpdateRole(dto.Id, dto);

        // Assert
        Assert.False(result.Success);
        Assert.False(result.Data);
        Assert.Equal(errorMessage, result.Error);
        mediatorMock.Verify(x => x.Send(new UserRoleUpdateCommand(dto), default), Times.Once);
    }
    #endregion
}
