using FluentAssertions;
using Jared.Api.Integration.Tests.Data;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.EpicDtos;
using Mapster;
using System.Net.Http.Json;

namespace Jared.Api.Integration.Tests.Tests.Epics.Page;

public class EpicPageTest : BaseIntegrationTest
{
    protected override string URL => "Epic/Page";

    public EpicPageTest(JaredWebApplicationFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task PageEpic_When_Page_1_PageSize_2_ShoutdBeSuccess()
    {
        // Arrange
        var query = "?page=1&pageSize=2";
        Result<EpicPageDto> expectedResponse = new(true, string.Empty)
        {
            Data = new()
            {
                Pagination = new()
                {
                    CurrentPage = 1,
                    ItemFrom = 1,
                    ItemTo = 2,
                    ItemsCount = 4,
                    PageCount = 2,
                    PageSize = 2,
                },
                Epics = new()
                {
                    EpicIntegrationFaker.FirstEpic.Adapt<EpicListDto>(),
                    EpicIntegrationFaker.SecondEpic.Adapt<EpicListDto>(),
                },
            },
        };

        // Act
        var response = await Client.GetFromJsonAsync<Result<EpicPageDto>>(URL + query, default);

        // Assert
        response.Should().BeEquivalentTo(
            expectedResponse,
            options => options.WithStrictOrderingFor(x => x.Data.Epics));
    }

    [Fact]
    public async Task PageEpic_When_Page_1_PageSize_2_SortingProperty_Id_SortingDirection_Descending_ShoutdBeSuccess()
    {
        // Arrange
        var query = "?page=1&pageSize=2&sortingProperty=Id&sortingDirection=Descending";
        Result<EpicPageDto> expectedResponse = new(true, string.Empty)
        {
            Data = new()
            {
                Pagination = new()
                {
                    CurrentPage = 1,
                    ItemFrom = 1,
                    ItemTo = 2,
                    ItemsCount = 4,
                    PageCount = 2,
                    PageSize = 2,
                },
                Epics = new()
                {
                    EpicIntegrationFaker.FourthEpic.Adapt<EpicListDto>(),
                    EpicIntegrationFaker.ThirdEpic.Adapt<EpicListDto>(),
                },
            },
        };

        // Act
        var response = await Client.GetFromJsonAsync<Result<EpicPageDto>>(URL + query, default);

        // Assert
        response.Should().BeEquivalentTo(
            expectedResponse,
            options => options.WithStrictOrderingFor(x => x.Data.Epics));
    }

    [Fact]
    public async Task PageEpic_When_Page_1_PageSize_10_Fileter_Title_First_ShoutdBeSuccess()
    {
        // Arrange
        var query = "?page=1&pageSize=10&Title=first";
        Result<EpicPageDto> expectedResponse = new(true, string.Empty)
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
                Epics = new()
                {
                    EpicIntegrationFaker.FirstEpic.Adapt<EpicListDto>(),
                },
            },
        };

        // Act
        var response = await Client.GetFromJsonAsync<Result<EpicPageDto>>(URL + query, default);

        // Assert
        response.Should().BeEquivalentTo(expectedResponse);
    }
}
