using FluentAssertions;
using Jared.Api.Integration.Tests.Data;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.ProjectDtos;
using Mapster;
using System.Net.Http.Json;

namespace Jared.Api.Integration.Tests.Tests.Projects.Update;

public class ProjectUpdateTest : BaseIntegrationTest
{
    protected override string URL => "Project/Update";

    public ProjectUpdateTest(JaredWebApplicationFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task UpdateProject_WhenAllDataIsValid_ShouldBeSuccess()
    {
        // Arrange
        var dto = ProjectIntegrationFaker.FirstProject.Adapt<ProjectDetailsDto>();
        dto.Description = "New description";

        Result<bool> expectedResponse = new(true, string.Empty)
        {
            Data = true,
        };

        // Act
        var result = await Client.PutAsJsonAsync(URL, dto, default);
        var response = await result.Content.ReadFromJsonAsync<Result<bool>>();

        // Assert
        response.Should().BeEquivalentTo(expectedResponse);
    }
}
