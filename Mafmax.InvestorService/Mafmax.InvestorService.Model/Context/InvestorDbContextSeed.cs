using System;
using System.Threading.Tasks;
using Mafmax.InvestorService.Model.Entities;
using Mafmax.InvestorService.Model.Entities.Assets;
using Microsoft.EntityFrameworkCore;

namespace Mafmax.InvestorService.Model.Context;

/// <summary>
/// Class to seed initial data into InvestorDbContext
/// </summary>
public static class InvestorDbContextSeed
{

    /// <summary>
    /// Extension method to DbContext seeding
    /// </summary>
    /// <param name="context">DbContext</param>
    /// <returns></returns>
    public async static Task SeedDatabaseAsync(this InvestorDbContext context)
    {
        if (await context.Assets.AnyAsync()) return;

        var countries = await SeedCountriesAsync(context);

        var industries = await SeedIndustriesAsync(context);

        var issuers = await SeedIssuersAsync(context, countries, industries);

        var stockExchanges = await SeedStockExchangesAsync(context);

        _ = await SeedAssetsAsync(context, issuers, stockExchanges);
    }

    private async static Task<StockExchangeEntity[]> SeedStockExchangesAsync(InvestorDbContext context)
    {
        await context.StockExchanges.AddRangeAsync(new StockExchangeEntity[]
        {
            new() {Key = "MOEX", Name = "Московская биржа"}
        });

        await context.SaveChangesAsync();

        return await context.StockExchanges.ToArrayAsync();
    }
    private async static Task<CountryEntity[]> SeedCountriesAsync(InvestorDbContext context)
    {
        await context.Countries.AddRangeAsync(new CountryEntity[]
        {
            new(){Name = "Россия"},
            new(){Name = "США"},
        });

        await context.SaveChangesAsync();

        return await context.Countries.ToArrayAsync();
    }

    private async static Task<IndustryEntity[]> SeedIndustriesAsync(InvestorDbContext context)
    {
        await context.Industries.AddRangeAsync(new IndustryEntity {Name = "Технологии"},
            new IndustryEntity {Name = "Финансы"}, new IndustryEntity {Name = "Добыча ископаемых"},
            new IndustryEntity {Name = "Нефть и газ"}, new IndustryEntity {Name = "Телекоммуникации"},
            new IndustryEntity {Name = "IT"});

        await context.SaveChangesAsync();

        return await context.Industries.ToArrayAsync();
    }
      
    private async static Task<IssuerEntity[]> SeedIssuersAsync(InvestorDbContext context, CountryEntity[] countries, IndustryEntity[] industries)
    {
        await context.Issuers.AddRangeAsync(
            new IssuerEntity {Country = countries[0], Industry = industries[4], Name = "МТС"},
            new IssuerEntity {Country = countries[0], Industry = industries[3], Name = "Сургутнефтегаз"},
            new IssuerEntity {Country = countries[0], Industry = industries[2], Name = "Алроса"},
            new IssuerEntity {Country = countries[1], Industry = industries[0], Name = "Apple"},
            new IssuerEntity {Country = countries[0], Industry = industries[5], Name = "Яндекс"},
            new IssuerEntity {Country = countries[0], Industry = industries[1], Name = "Сбербанк России"});
          
        await context.SaveChangesAsync();
           
        return await context.Issuers.ToArrayAsync();
    }
      
    private async static Task<AssetEntity[]> SeedAssetsAsync(InvestorDbContext context, IssuerEntity[] issuers, StockExchangeEntity[] stockExchanges)
    {
        var circulations = new CirculationPeriodEntity[]
        {
            new() {Start = new DateTime(2016, 7, 16)},
            new() {Start = new DateTime(2004, 2, 11)},
            new() {Start = new DateTime(2005, 1, 11)},
            new() {Start = new DateTime(2011, 11, 29)},
            new() {Start = new DateTime(2020, 9, 8)},
            new() {Start = new DateTime(2014, 6, 4)},
            new() {Start = new DateTime(2007, 7, 20)},
            new() {Start = new DateTime(2020, 1, 20)},
            new() {Start = new DateTime(2019, 6, 19)}
        };

        await context.Assets.AddRangeAsync(
            new ShareEntity() { Issuer = issuers[5], Ticker = "SBERP", Isin = "RU0009029557", Name = "Сбербанк России, акция привелегированная", IsPreferred = true, LotSize = 10, Currency = "RUB", Stock = stockExchanges[0], Circulation = circulations[0] },
            new ShareEntity() { Issuer = issuers[0], Ticker = "MTSS", Isin = "RU0007775219", Name = "МТС, акция обыкновенная", IsPreferred = false, LotSize = 10, Currency = "RUB", Stock = stockExchanges[0], Circulation = circulations[1] },
            new ShareEntity() { Issuer = issuers[1], Ticker = "SNGS", Isin = "RU0008926258", Name = "Сургутнефтегаз, акция обыкновенная", IsPreferred = false, LotSize = 100, Currency = "RUB", Stock = stockExchanges[0], Circulation = circulations[2] },
            new ShareEntity() { Issuer = issuers[2], Ticker = "ALRS", Isin = "RU0007252813", Name = "Алроса, акция обыкновенная", IsPreferred = false, LotSize = 10, Currency = "RUB", Stock = stockExchanges[0], Circulation = circulations[3] },
            new ShareEntity() { Issuer = issuers[3], Ticker = "AAPL-RM", Isin = "US0378331005", Name = "Apple, акция обыкновенная", IsPreferred = false, LotSize = 1, Currency = "USD", Stock = stockExchanges[0], Circulation = circulations[4] },
            new ShareEntity() { Issuer = issuers[4], Ticker = "YNDX", Isin = "NL0009805522", Name = "ЯНДЕКС Н.В., акция обыкновенная", IsPreferred = false, LotSize = 1, Currency = "EUR", Stock = stockExchanges[0], Circulation = circulations[5] },
            new ShareEntity() { Issuer = issuers[5], Ticker = "SBER", Isin = "RU0009029540", Name = "Сбербанк России, акция обыкновенная", IsPreferred = false, LotSize = 10, Currency = "RUB", Stock = stockExchanges[0], Circulation = circulations[6] },
            new BondEntity() { Issuer = issuers[5], Ticker = "RU000A101C89", Isin = "RU000A101C89", Name = "Сбербанк ПАО 001Р-SBER15", LotSize = 1, Currency = "RUB", Stock = stockExchanges[0], Type = BondType.Corporate, Circulation = circulations[7] },
            new BondEntity() { Issuer = issuers[5], Ticker = "RU000A1013J4", Isin = "RU000A1013J4", Name = "СберИОС 001Р-177R GMKN 100", LotSize = 1, Currency = "RUB", Stock = stockExchanges[0], Type = BondType.Corporate, Circulation = circulations[8] }
        );
           
        await context.SaveChangesAsync();
         
        return await context.Assets.ToArrayAsync();
    }
}