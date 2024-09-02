using FluentAssertions;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.ProjectDtos;
using System.Net.Http.Json;

namespace Jared.Api.Integration.Tests.Tests.Projects.Create;

public class ProjectCreateTest : BaseIntegrationTest
{
    protected override string URL => "Project/Create";

    public ProjectCreateTest(JaredWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task CreateProject_WhenAllDataIsValid_ShouldBeSuccess()
    {
        // Arrange
        ProjectDetailsDto dto = new()
        {
            Title = "Test project",
            Code = "TP",
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
