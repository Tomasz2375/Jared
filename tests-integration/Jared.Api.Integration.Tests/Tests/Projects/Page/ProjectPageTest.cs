using FluentAssertions;
using Jared.Api.Integration.Tests.Data;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.ProjectDtos;
using Mapster;
using System.Net.Http.Json;

namespace Jared.Api.Integration.Tests.Tests.Projects.Page;

public class ProjectPageTest : BaseIntegrationTest
{
    protected override string URL => "Project/Page";

    public ProjectPageTest(JaredWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task PageProject_When_Page_1_PageSize_2_ShoutdBeSuccess()
    {
        // Arrange
        var query = "?page=1&pageSize=2";
        Result<ProjectPageDto> expectedResponse = new(true, string.Empty)
        {
            Data = new()
            {
                Pagination = new()
                {
                    CurrentPage = 1,
                    ItemFrom = 1,
                    ItemTo = 2,
                    ItemsCount = 3,
                    PageCount = 2,
                    PageSize = 2,
                },
                Projects = new()
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
            options => options.WithStrictOrderingFor(x => x.Data.Projects));
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
                Pagination = new()
                {
                    CurrentPage = 1,
                    ItemFrom = 1,
                    ItemTo = 2,
                    ItemsCount = 3,
                    PageCount = 2,
                    PageSize = 2,
                },
                Projects = new()
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
            options => options.WithStrictOrderingFor(x => x.Data.Projects));
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
                Pagination = new()
                {
                    CurrentPage = 1,
                    ItemFrom = 1,
                    ItemTo = 1,
                    ItemsCount = 1,
                    PageCount = 1,
                    PageSize = 10,
                },
                Projects = new()
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
