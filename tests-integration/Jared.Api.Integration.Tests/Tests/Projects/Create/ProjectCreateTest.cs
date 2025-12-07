using System.Net.Http.Json;
using FluentAssertions;
using Jared.Core.Abstractions;
using Jared.Dtos.Projects;

namespace Jared.Api.Integration.Tests.Tests.Projects.Create;

public class ProjectCreateTest(JaredWebApplicationFactory factory) : BaseIntegrationTest(factory)
{
    protected override string URL => "projects";

    [Fact]
    public async Task CreateProject_WhenAllDataIsValid_ShouldBeSuccess()
    {
        // Arrange
        ProjectDetailsDto dto = new()
        {
            Title = "Test project",
            Code = "TP",
            Color = "#FFBB99",
            Description = "Project description",
        };

        Result<bool> expectedResponse = new(true, string.Empty)
        {
            Data = true,
        };

        // Act
        var result = await Client.PostAsJsonAsync(URL, dto, default);
        var response = await result.Content.ReadFromJsonAsync<Result<bool>>();

        // Assert
        response.Should().BeEquivalentTo(expectedResponse);
    }
}
