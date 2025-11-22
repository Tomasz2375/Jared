using System.Net.Http.Json;
using FluentAssertions;
using Jared.Api.Integration.Tests.Data;
using Jared.Dtos.Epics;
using Jared.Shared.Abstractions;
using Mapster;

namespace Jared.Api.Integration.Tests.Tests.Epics.List;

public class EpicListTest(JaredWebApplicationFactory factory) : BaseIntegrationTest(factory)
{
    protected override string URL => "Epic/List";

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
