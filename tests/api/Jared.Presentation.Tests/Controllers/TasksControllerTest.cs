using AutoFixture.Xunit2;
using Jared.Contracts.Tasks;
using Jared.Core.Abstractions;
using Jared.Core.Enums;
using Jared.Dtos.Tasks;
using Jared.Presentation.Controllers;
using MediatR;
using Moq;

namespace Jared.Application.Tests.Controllers
{
    public class TasksControllerTest
    {
        private readonly TasksController controller;
        private readonly Mock<IMediator> mediatorMock = new();

        public TasksControllerTest() => controller = new(mediatorMock.Object);

        #region GetById
        [Theory]
        [AutoData]
        public async Task GetById_WnenMediatrReturnsOk_ShouldReturnSuccessResult(TaskDetailsDto dto)
        {
            // Arrange
            mediatorMock.Setup(x => x.Send(It.IsAny<TaskDetailsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(Result.Ok(dto)));

            // Act
            var result = await controller.GetById(dto.Id);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(dto, result.Data);
            Assert.Equal(string.Empty, result.Error);
            mediatorMock.Verify(x => x.Send(new TaskDetailsQuery(dto.Id), default), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task GetById_WnenMediatrReturnsFail_ShouldReturnFailureResult(string errorMessage)
        {
            // Arrange
            mediatorMock.Setup(x => x.Send(It.IsAny<TaskDetailsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(Result.Fail<TaskDetailsDto>(errorMessage)));

            // Act
            var result = await controller.GetById(1);

            // Assert
            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal(errorMessage, result.Error);
            mediatorMock.Verify(x => x.Send(new TaskDetailsQuery(1), default), Times.Once);
        }
        #endregion

        #region GetAll
        [Theory]
        [AutoData]
        public async Task GetAll_WnenMediatrReturnsOk_ShouldReturnSuccessResult(
            TaskPageDto dto,
            int page,
            int pageSize,
            string sortingProperty,
            SortingDirection sortingDirection,
            IDictionary<string, string?>? filter)
        {
            // Arrange
            mediatorMock.Setup(x => x.Send(It.IsAny<TaskPageQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(Result.Ok(dto)));

            // Act
            var result = await controller.GetAll(page, pageSize, sortingProperty, sortingDirection, filter);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(dto, result.Data);
            Assert.Equal(string.Empty, result.Error);
            mediatorMock.Verify(
                x => x.Send(new TaskPageQuery(page, pageSize, sortingProperty, sortingDirection, filter), default),
                Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task GetAll_WnenMediatrReturnsFail_ShouldReturnFailureResult(
            string errorMessage,
            int page,
            int pageSize,
            string sortingProperty,
            SortingDirection sortingDirection,
            IDictionary<string, string?>? filter)
        {
            // Arrange
            mediatorMock.Setup(x => x.Send(It.IsAny<TaskPageQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(Result.Fail<TaskPageDto>(errorMessage)));

            // Act
            var result = await controller.GetAll(page, pageSize, sortingProperty, sortingDirection, filter);

            // Assert
            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal(errorMessage, result.Error);
            mediatorMock.Verify(
                x => x.Send(new TaskPageQuery(page, pageSize, sortingProperty, sortingDirection, filter), default),
                Times.Once);
        }
        #endregion

        #region Update
        [Theory]
        [AutoData]
        public async Task Update_WnenMediatrReturnsOk_ShouldReturnSuccessResult(TaskDetailsDto dto)
        {
            // Arrange
            mediatorMock.Setup(x => x.Send(It.IsAny<TaskUpdateCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(Result.Ok(dto)));

            // Act
            var result = await controller.Update(dto.Id, dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(dto, result.Data);
            Assert.Equal(string.Empty, result.Error);
            mediatorMock.Verify(x => x.Send(new TaskUpdateCommand(dto), default), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task Update_WnenMediatrReturnsFail_ShouldReturnFailureResult(
            TaskDetailsDto dto,
            string errorMessage)
        {
            // Arrange
            mediatorMock.Setup(x => x.Send(It.IsAny<TaskUpdateCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(Result.Fail<TaskDetailsDto>(errorMessage)));

            // Act
            var result = await controller.Update(dto.Id, dto);

            // Assert
            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal(errorMessage, result.Error);
            mediatorMock.Verify(x => x.Send(new TaskUpdateCommand(dto), default), Times.Once);
        }
        #endregion

        #region Create
        [Theory]
        [AutoData]
        public async Task Create_WnenMediatrReturnsOk_ShouldReturnSuccessResult(TaskDetailsDto dto)
        {
            // Arrange
            mediatorMock.Setup(x => x.Send(It.IsAny<TaskCreateCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(Result.Ok(true)));

            // Act
            var result = await controller.Create(dto);

            // Assert
            Assert.True(result.Success);
            Assert.True(result.Data);
            Assert.Equal(string.Empty, result.Error);
            mediatorMock.Verify(x => x.Send(new TaskCreateCommand(dto), default), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task Create_WnenMediatrReturnsFail_ShouldReturnFailureResult(
            TaskDetailsDto dto,
            string errorMessage)
        {
            // Arrange
            mediatorMock.Setup(x => x.Send(It.IsAny<TaskCreateCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(Result.Fail<bool>(errorMessage)));

            // Act
            var result = await controller.Create(dto);

            // Assert
            Assert.False(result.Success);
            Assert.False(result.Data);
            Assert.Equal(errorMessage, result.Error);
            mediatorMock.Verify(x => x.Send(new TaskCreateCommand(dto), default), Times.Once);
        }
        #endregion
    }
}
