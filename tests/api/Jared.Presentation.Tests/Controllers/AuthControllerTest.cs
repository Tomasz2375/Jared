using AutoFixture.Xunit2;
using Jared.Contracts.Users;
using Jared.Core.Abstractions;
using Jared.Dtos.Users;
using Jared.Presentation.Controllers;
using MediatR;
using Moq;

namespace Jared.Application.Tests.Controllers;

public class AuthControllerTest
{
    private readonly AuthController controller;
    private readonly Mock<IMediator> mediatorMock = new();

    public AuthControllerTest() => controller = new(mediatorMock.Object);

    #region UserRegisterAsync
    [Theory]
    [AutoData]
    public async Task UserRegisterAsync_WnenMediatrReturnsOk_ShouldReturnSuccessResult(UserRegisterDto dto)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<UserRegisterCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Ok(true)));

        // Act
        var result = await controller.UserRegisterAsync(dto);

        // Assert
        Assert.True(result.Success);
        Assert.True(result.Data);
        Assert.Equal(string.Empty, result.Error);
        mediatorMock.Verify(x => x.Send(new UserRegisterCommand(dto), default), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task UserRegisterAsync_WnenMediatrReturnsFail_ShouldReturnFailureResult(
        UserRegisterDto dto,
        string errorMessage)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<UserRegisterCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Fail<bool>(errorMessage)));

        // Act
        var result = await controller.UserRegisterAsync(dto);

        // Assert
        Assert.False(result.Success);
        Assert.False(result.Data);
        Assert.Equal(errorMessage, result.Error);
        mediatorMock.Verify(x => x.Send(new UserRegisterCommand(dto), default), Times.Once);
    }
    #endregion

    #region UserLoginAsync
    [Theory]
    [AutoData]
    public async Task UserLoginAsync_WnenMediatrReturnsOk_ShouldReturnSuccessResult(UserLoginDto dto, string token)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<UserLoginCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Ok(token)));

        // Act
        var result = await controller.UserLoginAsync(dto);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(token, result.Data);
        Assert.Equal(string.Empty, result.Error);
        mediatorMock.Verify(x => x.Send(new UserLoginCommand(dto), default), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task UserLoginAsync_WnenMediatrReturnsFail_ShouldReturnFailureResult(
        UserLoginDto dto,
        string errorMessage)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<UserLoginCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Fail<string>(errorMessage)));

        // Act
        var result = await controller.UserLoginAsync(dto);

        // Assert
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal(errorMessage, result.Error);
        mediatorMock.Verify(x => x.Send(new UserLoginCommand(dto), default), Times.Once);
    }
    #endregion
}
