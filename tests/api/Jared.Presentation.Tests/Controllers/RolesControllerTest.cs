using AutoFixture.Xunit2;
using Jared.Contracts.Roles;
using Jared.Core.Abstractions;
using Jared.Dtos.Roles;
using Jared.Presentation.Controllers;
using MediatR;
using Moq;

namespace Jared.Presentation.Tests.Controllers;

public class RolesControllerTest
{
    private readonly RolesController controller;
    private readonly Mock<IMediator> mediatorMock = new();

    public RolesControllerTest() => controller = new(mediatorMock.Object);

    #region GetAll
    [Theory]
    [AutoData]
    public async Task GetAll_WnenMediatrReturnsOk_ShouldReturnSuccessResult(List<RoleListDto> dtos)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<RoleListQuery>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Ok(dtos)));

        // Act
        var result = await controller.GetAll();

        // Assert
        Assert.True(result.Success);
        Assert.Equal(dtos, result.Data);
        Assert.Equal(string.Empty, result.Error);
        mediatorMock.Verify(x => x.Send(new RoleListQuery(), default), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task GetAll_WnenMediatrReturnsFail_ShouldReturnFailureResult(string errorMessage)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<RoleListQuery>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Fail<List<RoleListDto>>(errorMessage)));

        // Act
        var result = await controller.GetAll();

        // Assert
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal(errorMessage, result.Error);
        mediatorMock.Verify(x => x.Send(new RoleListQuery(), default), Times.Once);
    }
    #endregion
}
