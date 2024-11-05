using FluentAssertions;
using Jared.Api.Integration.Tests.Data;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.EpicDtos;
using Mapster;
using System.Net.Http.Json;

namespace Jared.Api.Integration.Tests.Tests.Epics.List;

public class EpicListTest : BaseIntegrationTest
{
    protected override string URL => "Epic/List";

    public EpicListTest(JaredWebApplicationFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task ListEpics_WhenProjectIdIsSet_ShoutdReturnEpicWithProject()
    {
        // Arrange
        Result<List<EpicListDto>> expectedResponse = new(true, string.Empty)
        {
            Data = new()
            {
                EpicIntegrationFaker.FirstEpic.Adapt<EpicListDto>(),
                EpicIntegrationFaker.SecondEpic.Adapt<EpicListDto>(),
            },
        };

        // Act
        var response = await Client.GetFromJsonAsync<Result<List<EpicListDto>>>(URL + "?projectId=1001", default);

        // Assert
        expectedResponse.Should().BeEquivalentTo(response);
    }

    [Fact]
    public async Task ListEpics_WhenProjectIdIsNotSet_ShoutdReturnAllEpic()
    {
        // Arrange
        Result<List<EpicListDto>> expectedResponse = new(true, string.Empty)
        {
            Data = new()
            {
                EpicIntegrationFaker.FirstEpic.Adapt<EpicListDto>(),
                EpicIntegrationFaker.SecondEpic.Adapt<EpicListDto>(),
                EpicIntegrationFaker.ThirdEpic.Adapt<EpicListDto>(),
                EpicIntegrationFaker.FourthEpic.Adapt<EpicListDto>(),
            },
        };

        // Act
        var response = await Client.GetFromJsonAsync<Result<List<EpicListDto>>>(URL, default);

        // Assert
        expectedResponse.Should().BeEquivalentTo(response);
    }
}
