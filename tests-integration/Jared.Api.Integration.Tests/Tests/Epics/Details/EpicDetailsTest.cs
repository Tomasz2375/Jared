using FluentAssertions;
using Jared.Api.Integration.Tests.Data;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.EpicDtos;
using Mapster;
using System.Net.Http.Json;

namespace Jared.Api.Integration.Tests.Tests.Epics.Details;

public class EpicDetailsTest(JaredWebApplicationFactory factory) : BaseIntegrationTest(factory)
{
    protected override string URL => "Epic";

    [Fact]
    public async Task DetailsEpic_WhenEpicExists_ShoutdBeSuccess()
    {
        // Arrange
        Result<EpicDetailsDto> expectedResponse = new(true, string.Empty)
        {
            Data = EpicIntegrationFaker.FirstEpic.Adapt<EpicDetailsDto>(),
        };

        // Act
        var response = await Client.GetFromJsonAsync<Result<EpicDetailsDto>>(URL + "/1001", default);

        // Assert
        response.Should().BeEquivalentTo(expectedResponse);
    }

    [Theory]
    [InlineData("/-1")]
    [InlineData("/999")]
    public async Task DetailsEpic_WhenEpicDoesNotExists_ShoutdNotBeSuccess(string query)
    {
        // Arrange
        Result<EpicDetailsDto> expectedResponse = new(false, "Sequence contains no elements.")
        {
            Data = null!,
        };

        // Act
        var response = await Client.GetFromJsonAsync<Result<EpicDetailsDto>>(URL + query, default);

        // Assert
        response.Should().BeEquivalentTo(expectedResponse);
    }
}
