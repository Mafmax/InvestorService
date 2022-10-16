using System.Threading.Tasks;
using Mafmax.InvestorService.Services.Exceptions;
using Mafmax.InvestorService.Services.Queries.Assets;
using Mafmax.InvestorService.Services.Queries.Handlers;
using Mafmax.InvestorService.Services.Tests.Queries.Base;
using Xunit;
using static Mafmax.InvestorService.Services.MockData.Context.MockProvider;

namespace Mafmax.InvestorService.Services.Tests.Queries;

public class AssetsQueriesHandlerTests : InvestorServiceQueriesHandlerTestsBase<AssetsQueriesHandler>
{
    protected override AssetsQueriesHandler GetHandler()
    {
        var db = GetContext();
        var mapper = GetMapper(typeof(AssetsQueriesHandler).Assembly);
        return new(db, mapper);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(9)]
    public async Task GetAssetById_ShouldReturnsAssetWithSameId(int id)
    {
        //Arrange
        GetAssetByIdQuery query = new(id);

        //Act
        var asset = await AskAsync(query);

        //Assert
        Assert.Equal(id, asset!.Id);
    }

    [Theory]
    [InlineData("US9872349871")]
    [InlineData("RU0AS235JS29")]
    [InlineData("RU6403729562")]
    public async Task GetAssetByIsin_ShouldReturnsAssetWithSameIsin(string isin)
    {
        //Arrange
        GetAssetByIsinQuery query = new(isin);

        //Act
        var asset = await AskAsync(query);

        //Assert
        Assert.Equal(isin, asset!.Isin);
    }

    [Fact]
    public async Task GetAssetById_ShouldThrow_IfNotFound()
    {
        //Arrange
        GetAssetByIdQuery query = new(777888999);

        //Act

        //Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(async () => await AskAsync(query));
    }

    [Fact]
    public async Task GetAssetByIsin_ShouldThrows_IfNotFound()
    {
        //Arrange
        GetAssetByIsinQuery query = new("AA1234567890");

        //Act

        //Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(async () => await AskAsync(query));
    }
    
    [Theory]
    [InlineData("Sber", "Акция")]
    [InlineData("Share1", "Облигация")]
    [InlineData("Bond1", "Акция")]
    public async Task FindAssetsWithClass_ShouldReturnsEmpty_IFNotFound(string searchString, string classFilter)
    {
        //Arrange
        FindAssetsWithClassQuery query = new(searchString, classFilter, MinimalSearchStringLength: 1);

        //Act
        var assets = await AskAsync(query);

        //Assert
        Assert.Empty(assets);
    }

    [Theory]
    [InlineData("Sber")]
    [InlineData("Shareee1")]
    [InlineData("Bond120")]
    public async Task FindAssets_ShouldReturnsEmpty_IFNotFound(string searchString)
    {
        //Arrange
        FindAssetsQuery query = new(searchString, MinimalSearchStringLength: 1);

        //Act
        var assets = await AskAsync(query);

        //Assert
        Assert.Empty(assets);
    }

    [Theory]
    [InlineData("Share")]
    [InlineData("RU0")]
    [InlineData("Ticker")]
    public async Task FindAssets_ShouldReturnsResult_IfSearchStringIsPartOfIsinOrNameOrTicker(string searchString)
    {
        //Arrange
        FindAssetsQuery query = new(searchString, MinimalSearchStringLength: 1);

        //Act
        var assets = await AskAsync(query);

        //Assert
        Assert.NotEmpty(assets);
    }

    [Theory]
    [InlineData("Share")]
    [InlineData("RU0")]
    [InlineData("Ticker")]
    public async Task FindAssetsWithClass_ShouldReturnsResult_IfSearchStringIsPartOfIsinOrNameOrTicker(string searchString)
    {
        //Arrange
        FindAssetsWithClassQuery query = new(searchString, AssetsClass: "Акция", MinimalSearchStringLength: 1);

        //Act
        var assets = await AskAsync(query);

        //Assert
        Assert.NotEmpty(assets);
    }

    [Theory]
    [InlineData(1000)]
    public async Task GetIssuerAssets_ShouldThrows_IfIssuerNotFound(int id)
    {
        //Arrange
        GetIssuerAssetsQuery query = new(id);

        //Act

        //Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(async () => await AskAsync(query));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GetIssuerAssets_ShouldReturnsResults_IfIssuerFound(int id)
    {
        //Arrange
        GetIssuerAssetsQuery query = new(id);

        //Act
        var assets = await AskAsync(query);

        //Assert
        Assert.NotEmpty(assets);
    }

    [Theory]
    [InlineData(1,1)]
    [InlineData(2,2)]
    public async Task GetIssuerAssets_ShouldReturnsAvailableAssets(int id,int expectedCount)
    {
        //Arrange
        GetIssuerAssetsQuery query = new(id);

        //Act
        var assets = await AskAsync(query);

        //Assert
        Assert.Equal(expectedCount,assets.Length);
    }

}