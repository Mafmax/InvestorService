using System;
using System.Reflection;
using System.Threading.Tasks;
using Mafmax.InvestorService.Model.Context;
using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Exceptions;
using Mafmax.InvestorService.Services.Services.Queries.Assets;
using Mafmax.InvestorService.Services.Services.Queries.Handlers;
using Mafmax.InvestorService.Services.Services.Queries.Interfaces;
using Mafmax.InvestorService.Services.Tests.Queries.Base;
using Xunit;
using static Mafmax.InvestorService.Services.MockData.Context.MockProvider;

namespace Mafmax.InvestorService.Services.Tests.Queries
{
    public class AssetsQueriesHandlerTests : InvestorServiceQueriesHandlerTestsBase<AssetsQueriesHandler>
    {
        protected override AssetsQueriesHandler GetHandler()
        {
            var db = GetContext("Readonly");
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
            AssetDto? asset = await Handler.AskAsync(query);

            //Assert
            Assert.Equal(id, asset!.Id);
        }

        [Theory]
        [InlineData("ISIN2")]
        [InlineData("ISIN5")]
        [InlineData("ISIN7")]
        public async Task GetAssetByIsin_ShouldReturnsAssetWithSameIsin(string isin)
        {
            //Arrange
            GetAssetByIsinQuery query = new(isin);

            //Act
            AssetDto? asset = await Handler.AskAsync(query);

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
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await Handler.AskAsync(query));
        }

        [Fact]
        public async Task GetAssetByIsin_ShouldThrows_IfNotFound()
        {
            //Arrange
            GetAssetByIsinQuery query = new("NotExistsIsin");

            //Act

            //Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await Handler.AskAsync(query));
        }

        [Theory]
        [InlineData("aa", 3)]
        [InlineData("aa", 10)]
        public async Task FindAssets_ShouldThrows_IfIncorrectData(string searchString, int minLength)
        {
            //Arrange
            FindAssetsQuery query = new(searchString, minLength);

            //Act

            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await Handler.AskAsync(query));

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
            ShortAssetDto[] assets = await Handler.AskAsync(query);

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
            ShortAssetDto[] assets = await Handler.AskAsync(query);

            //Assert
            Assert.Empty(assets);
        }

        [Theory]
        [InlineData("Share")]
        [InlineData("ISIN")]
        [InlineData("Ticker")]
        public async Task FindAssets_ShouldReturnsResult_IfSearchStringIsPartOfIsinOrNameOrTicker(string searchString)
        {
            //Arrange
            FindAssetsQuery query = new(searchString, MinimalSearchStringLength: 1);

            //Act
            ShortAssetDto[] assets = await Handler.AskAsync(query);

            //Assert
            Assert.NotEmpty(assets);
        }

        [Theory]
        [InlineData("Share")]
        [InlineData("ISIN")]
        [InlineData("Ticker")]
        public async Task FindAssetsWithClass_ShouldReturnsResult_IfSearchStringIsPartOfIsinOrNameOrTicker(string searchString)
        {
            //Arrange
            FindAssetsWithClassQuery query = new(searchString, AssetsClass: "Акция", MinimalSearchStringLength: 1);

            //Act
            ShortAssetDto[] assets = await Handler.AskAsync(query);

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
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await Handler.AskAsync(query));
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
            ShortAssetDto[] assets = await Handler.AskAsync(query);

            //Assert
            Assert.NotEmpty(assets);
        }



    }
}
