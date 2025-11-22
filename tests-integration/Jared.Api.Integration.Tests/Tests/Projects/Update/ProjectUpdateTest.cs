using System.Net.Http.Json;
using FluentAssertions;
using Jared.Api.Integration.Tests.Data;
using Jared.Dtos.Projects;
using Jared.Shared.Abstractions;
using Mapster;

namespace Jared.Api.Integration.Tests.Tests.Projects.Update;

public class ProjectUpdateTest(JaredWebApplicationFactory factory) : BaseIntegrationTest(factory)
{
    protected override string URL => "Project/Update";

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
