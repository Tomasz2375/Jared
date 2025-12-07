using System.Net.Http.Json;
using FluentAssertions;
using Jared.Api.Integration.Tests.Data;
using Jared.Core.Abstractions;
using Jared.Dtos.Projects;
using Mapster;

namespace Jared.Api.Integration.Tests.Tests.Projects.Page;

public class ProjectPageTest(JaredWebApplicationFactory factory) : BaseIntegrationTest(factory)
{
    protected override string URL => "projects";

    [Fact]
    public async Task PageProject_When_Page_1_PageSize_2_ShoutdBeSuccess()
    {
        // Arrange
        var query = "?page=1&pageSize=2";
        Result<ProjectPageDto> expectedResponse = new(true, string.Empty)
        {
            Data = new()
            {
                Page = 1,
                PageSize = 2,
                TotalItems = 3,
                TotalPages = 2,
                Items = new()
                {
                    ProjectIntegrationFaker.FirstProject.Adapt<ProjectListDto>(),
                    ProjectIntegrationFaker.SecondProject.Adapt<ProjectListDto>(),
                },
            },
        };

        // Act
        var response = await Client.GetFromJsonAsync<Result<ProjectPageDto>>(URL + query, default);

        // Assert
        response.Should().BeEquivalentTo(
            expectedResponse,
            options => options.WithStrictOrderingFor(x => x.Data.Items));
    }

    [Fact]
    public async Task PageProject_When_Page_1_PageSize_2_SortingProperty_Id_SortingDirection_Descending_ShoutdBeSuccess()
    {
        // Arrange
        var query = "?page=1&pageSize=2&sortingProperty=Id&sortingDirection=Descending";
        Result<ProjectPageDto> expectedResponse = new(true, string.Empty)
        {
            Data = new()
            {
                Page = 1,
                PageSize = 2,
                TotalItems = 3,
                TotalPages = 2,
                Items = new()
                {
                    ProjectIntegrationFaker.ThirdProject.Adapt<ProjectListDto>(),
                    ProjectIntegrationFaker.SecondProject.Adapt<ProjectListDto>(),
                },
            },
        };

        // Act
        var response = await Client.GetFromJsonAsync<Result<ProjectPageDto>>(URL + query, default);

        // Assert
        response.Should().BeEquivalentTo(
            expectedResponse,
            options => options.WithStrictOrderingFor(x => x.Data.Items));
    }

    [Fact]
    public async Task PageProject_When_Page_1_PageSize_10_Fileter_Title_First_ShoutdBeSuccess()
    {
        // Arrange
        var query = "?page=1&pageSize=10&Title=first";
        Result<ProjectPageDto> expectedResponse = new(true, string.Empty)
        {
            Data = new()
            {
                Page = 1,
                PageSize = 10,
                TotalItems = 1,
                TotalPages = 1,
                Items = new()
                {
                    ProjectIntegrationFaker.FirstProject.Adapt<ProjectListDto>(),
                },
            },
        };

        // Act
        var response = await Client.GetFromJsonAsync<Result<ProjectPageDto>>(URL + query, default);

        // Assert
        response.Should().BeEquivalentTo(expectedResponse);
    }
}
