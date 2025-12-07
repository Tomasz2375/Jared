using AutoFixture.Xunit2;
using Jared.Contracts.Tasks;
using Jared.Contracts.Worklogs;
using Jared.Core.Abstractions;
using Jared.Core.Enums;
using Jared.Dtos.Tasks;
using Jared.Dtos.WorkLogs;
using Jared.Presentation.Controllers;
using MediatR;
using Moq;

namespace Jared.Application.Tests.Controllers
{
    public class WorkLogControllerTest
    {
        private readonly WorkLogsController controller;
        private readonly Mock<IMediator> mediatorMock = new();

        public WorkLogControllerTest() => controller = new(mediatorMock.Object);

        #region GetAll
        [Theory]
        [AutoData]
        public async Task GetAll_WnenMediatrReturnsOk_ShouldReturnSuccessResult(
            List<WorkLogStatisticsDto> dtos,
            int userId,
            int month,
            int year)
        {
            // Arrange
            mediatorMock.Setup(x => x.Send(It.IsAny<WorkLogStatisticsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(Result.Ok(dtos)));

            // Act
            var result = await controller.GetAll(userId, month, year);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(dtos, result.Data);
            Assert.Equal(string.Empty, result.Error);
            mediatorMock.Verify(
                x => x.Send(new WorkLogStatisticsQuery(userId, month, year), default),
                Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task GetAll_WnenMediatrReturnsFail_ShouldReturnFailureResult(
            string errorMessage,
            int userId,
            int month,
            int year)
        {
            // Arrange
            mediatorMock.Setup(x => x.Send(It.IsAny<WorkLogStatisticsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(Result.Fail<List<WorkLogStatisticsDto>>(errorMessage)));

            // Act
            var result = await controller.GetAll(userId, month, year);

            // Assert
            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal(errorMessage, result.Error);
            mediatorMock.Verify(
                x => x.Send(new WorkLogStatisticsQuery(userId, month, year), default),
                Times.Once);
        }
        #endregion
    }
}
