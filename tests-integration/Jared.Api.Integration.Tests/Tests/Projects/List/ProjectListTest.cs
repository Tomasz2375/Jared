using FluentAssertions;
using Jared.Api.Integration.Tests.Data;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.ProjectDtos;
using Mapster;
using System.Net.Http.Json;

namespace Jared.Api.Integration.Tests.Tests.Projects.List;

public class ProjectListTest(JaredWebApplicationFactory factory) : BaseIntegrationTest(factory)
{
    protected override string URL => "Project/List";

    [Fact]
    public async Task ListProjects_WhenProjectExists_ShoutdBeSuccess()
    {
        // Arrange
        Result<List<ProjectListDto>> expectedResponse = new(true, string.Empty)
        {
            Data = new()
            {
                ProjectIntegrationFaker.FirstProject.Adapt<ProjectListDto>(),
                ProjectIntegrationFaker.SecondProject.Adapt<ProjectListDto>(),
                ProjectIntegrationFaker.ThirdProject.Adapt<ProjectListDto>(),
            },
        };

        // Act
        var response = await Client.GetFromJsonAsync<Result<List<ProjectListDto>>>(URL, default);

        // Assert
        expectedResponse.Should().BeEquivalentTo(response);
    }
}
