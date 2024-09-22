using FluentAssertions;
using Jared.Api.Integration.Tests.Data;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.EpicDtos;
using Jared.Shared.Enums;
using System.Net.Http.Json;

namespace Jared.Api.Integration.Tests.Tests.Epics.Create;

public class EpicCreateTest : BaseIntegrationTest
{
    protected override string URL => "Epic/Create";

    public EpicCreateTest(JaredWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task CreateEpic_WhenAllDataIsValid_ShouldBeSuccess()
    {
        // Arrange
        var dto = validDto();

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

    [Fact]
    public async Task CreateEpic_WhenTitleIsEmpty_ShouldReturnErrorResponse()
    {
        // Arrange
        var dto = validDto();
        dto.Title = string.Empty;

        ErrorResponse expectedResponse = new()
        {
            Status = 400,
            Errors = new Dictionary<string, string[]>
            {
                { "Title", new[] { "'Title' must not be empty." } }
            }
        };

        // Act
        var result = await Client.PostAsJsonAsync(URL, dto, default);
        var response = await result.Content.ReadFromJsonAsync<ErrorResponse>();

        // Assert
        response.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task CreateEpic_WhenTitleIsToLong_ShouldReturnErrorResponse()
    {
        // Arrange
        var dto = validDto();
        dto.Title = string.Concat(Enumerable.Repeat(".", 101));

        ErrorResponse expectedResponse = new()
        {
            Status = 400,
            Errors = new Dictionary<string, string[]>
            {
                { "Title", new[] { "The length of 'Title' must be 100 characters or fewer. You entered 101 characters." } }
            }
        };

        // Act
        var result = await Client.PostAsJsonAsync(URL, dto, default);
        var response = await result.Content.ReadFromJsonAsync<ErrorResponse>();

        // Assert
        response.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task CreateEpic_WhenProjectIdIsEmpty_ShouldReturnErrorResponse()
    {
        // Arrange
        var dto = validDto();
        dto.ProjectId = 0;

        ErrorResponse expectedResponse = new()
        {
            Status = 400,
            Errors = new Dictionary<string, string[]>
            {
                { "ProjectId", new[] { "'Project Id' must not be empty." } }
            }
        };

        // Act
        var result = await Client.PostAsJsonAsync(URL, dto, default);
        var response = await result.Content.ReadFromJsonAsync<ErrorResponse>();

        // Assert
        response.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task CreateEpic_StatusIsNotInEnum_ShouldReturnErrorResponse()
    {
        // Arrange
        var dto = validDto();
        dto.Status = (EpicStatus)100;

        ErrorResponse expectedResponse = new()
        {
            Status = 400,
            Errors = new Dictionary<string, string[]>
            {
                { "Status", new[] { "'Status' has a range of values which does not include '100'." } }
            }
        };

        // Act
        var result = await Client.PostAsJsonAsync(URL, dto, default);
        var response = await result.Content.ReadFromJsonAsync<ErrorResponse>();

        // Assert
        response.Should().BeEquivalentTo(expectedResponse);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task CreateEpic_ParentIdIsNotValid_ShouldReturnErrorResponse(int parentId)
    {
        // Arrange
        var dto = validDto();
        dto.ParentId = parentId;

        ErrorResponse expectedResponse = new()
        {
            Status = 400,
            Errors = new Dictionary<string, string[]>
            {
                { "ParentId", new[] { "The specified condition was not met for 'Parent Id'." } }
            }
        };

        // Act
        var result = await Client.PostAsJsonAsync(URL, dto, default);
        var response = await result.Content.ReadFromJsonAsync<ErrorResponse>();

        // Assert
        response.Should().BeEquivalentTo(expectedResponse);
    }

    private EpicDetailsDto validDto() => new()
    {
        Title = "Test epic",
        ProjectId = ProjectIntegrationFaker.FirstProject.Id,
        Status = EpicStatus.Created,
        Description = "Project description",
    };
}
