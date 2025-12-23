using AutoFixture.Xunit2;
using Jared.Contracts.Auth;
using Jared.Core.Abstractions;
using Jared.Dtos.Auth;
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
    public async Task UserRegisterAsync_WnenMediatrReturnsOk_ShouldReturnSuccessResult(RegisterRequestDto dto)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Ok(true)));

        // Act
        var result = await controller.UserRegisterAsync(dto);

        // Assert
        Assert.True(result.Success);
        Assert.True(result.Data);
        Assert.Equal(string.Empty, result.Error);
        mediatorMock.Verify(x => x.Send(new RegisterCommand(dto), default), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task UserRegisterAsync_WnenMediatrReturnsFail_ShouldReturnFailureResult(
        RegisterRequestDto dto,
        string errorMessage)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Fail<bool>(errorMessage)));

        // Act
        var result = await controller.UserRegisterAsync(dto);

        // Assert
        Assert.False(result.Success);
        Assert.False(result.Data);
        Assert.Equal(errorMessage, result.Error);
        mediatorMock.Verify(x => x.Send(new RegisterCommand(dto), default), Times.Once);
    }
    #endregion

    #region UserLoginAsync
    [Theory]
    [AutoData]
    public async Task UserLoginAsync_WnenMediatrReturnsOk_ShouldReturnSuccessResult(LoginRequestDto dto, LoginResponseDto response)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Ok(response)));

        // Act
        var result = await controller.UserLoginAsync(dto);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(response, result.Data);
        Assert.Equal(string.Empty, result.Error);
        mediatorMock.Verify(x => x.Send(new LoginCommand(dto), default), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task UserLoginAsync_WnenMediatrReturnsFail_ShouldReturnFailureResult(
        LoginRequestDto dto,
        string errorMessage)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Fail<LoginResponseDto>(errorMessage)));

        // Act
        var result = await controller.UserLoginAsync(dto);

        // Assert
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal(errorMessage, result.Error);
        mediatorMock.Verify(x => x.Send(new LoginCommand(dto), default), Times.Once);
    }
    #endregion
}
