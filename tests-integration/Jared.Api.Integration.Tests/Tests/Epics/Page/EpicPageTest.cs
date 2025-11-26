using System.Net.Http.Json;
using FluentAssertions;
using Jared.Api.Integration.Tests.Data;
using Jared.Dtos.Epics;
using Jared.Shared.Abstractions;
using Mapster;

namespace Jared.Api.Integration.Tests.Tests.Epics.Page;

public class EpicPageTest(JaredWebApplicationFactory factory) : BaseIntegrationTest(factory)
{
    protected override string URL => "Epic/Page";

    [Fact]
    public async Task PageEpic_When_Page_1_PageSize_2_ShoutdBeSuccess()
    {
        // Arrange
        var query = "?page=1&pageSize=2";
        Result<EpicPageDto> expectedResponse = new(true, string.Empty)
        {
            Data = new()
            {
                Page = 1,
                PageSize = 2,
                TotalItems = 4,
                TotalPages = 2,
                Items = new()
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
            options => options.WithStrictOrderingFor(x => x.Data.Items));
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
                Page = 1,
                PageSize = 2,
                TotalItems = 4,
                TotalPages = 2,
                Items = new()
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
            options => options.WithStrictOrderingFor(x => x.Data.Items));
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
                Page = 1,
                PageSize = 10,
                TotalItems = 1,
                TotalPages = 1,
                Items = new()
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
