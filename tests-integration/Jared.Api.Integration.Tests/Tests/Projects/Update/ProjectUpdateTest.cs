using System.Net.Http.Json;
using FluentAssertions;
using Jared.Api.Integration.Tests.Data;
using Jared.Core.Abstractions;
using Jared.Dtos.Projects;
using Mapster;

namespace Jared.Api.Integration.Tests.Tests.Projects.Update;

public class ProjectUpdateTest(JaredWebApplicationFactory factory) : BaseIntegrationTest(factory)
{
    protected override string URL => "projects";

    [Fact]
    public async Task UpdateProject_WhenAllDataIsValid_ShouldBeSuccess()
    {
        // Arrange
        var dto = ProjectIntegrationFaker.FirstProject.Adapt<ProjectDetailsDto>();
        dto.Description = "New description";
        var path = $"{URL}/{dto.Id}";

        Result<bool> expectedResponse = new(true, string.Empty)
        {
            Data = true,
        };

        // Act
        var result = await Client.PutAsJsonAsync(path, dto, default);
        var response = await result.Content.ReadFromJsonAsync<Result<bool>>();

        // Assert
        response.Should().BeEquivalentTo(expectedResponse);
    }
}
