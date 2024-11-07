using FluentAssertions;
using Jared.Api.Integration.Tests.Data;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.ProjectDtos;
using Mapster;
using System.Net.Http.Json;

namespace Jared.Api.Integration.Tests.Tests.Projects.Details;

public class ProjectDetailsTest(JaredWebApplicationFactory factory) : BaseIntegrationTest(factory)
{
    protected override string URL => "Project";

    [Fact]
    public async Task DetailsProject_WhenProjectExists_ShoutdBeSuccess()
    {
        // Arrange
        Result<ProjectDetailsDto> expectedResponse = new(true, string.Empty)
        {
            Data = ProjectIntegrationFaker.FirstProject.Adapt<ProjectDetailsDto>(),
        };

        // Act
        var response = await Client.GetFromJsonAsync<Result<ProjectDetailsDto>>(URL + "/1001", default);

        // Assert
        response.Should().BeEquivalentTo(expectedResponse);
    }

    [Theory]
    [InlineData("/-1")]
    [InlineData("/999")]
    public async Task DetailsProject_WhenProjectDoesNotExists_ShoutdNotBeSuccess(string query)
    {
        // Arrange
        Result<ProjectDetailsDto> expectedResponse = new(false, "Sequence contains no elements.")
        {
            Data = null!,
        };

        // Act
        var response = await Client.GetFromJsonAsync<Result<ProjectDetailsDto>>(URL + query, default);

        // Assert
        response.Should().BeEquivalentTo(expectedResponse);
    }
}
