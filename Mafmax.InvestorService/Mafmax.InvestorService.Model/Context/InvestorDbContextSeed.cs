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
            new("MOEX", "Московская биржа")
        });

        await context.SaveChangesAsync();

        return await context.StockExchanges.ToArrayAsync();
    }
    private async static Task<CountryEntity[]> SeedCountriesAsync(InvestorDbContext context)
    {
        await context.Countries.AddRangeAsync(new CountryEntity[]
        {
            new("Россия"),
            new("США"),
        });

        await context.SaveChangesAsync();

        return await context.Countries.ToArrayAsync();
    }

    private async static Task<IndustryEntity[]> SeedIndustriesAsync(InvestorDbContext context)
    {
        await context.Industries.AddRangeAsync(new IndustryEntity("Технологии"),
            new IndustryEntity("Финансы"), new IndustryEntity("Добыча ископаемых"),
            new IndustryEntity("Нефть и газ"), new IndustryEntity("Телекоммуникации"),
            new IndustryEntity("IT"));

        await context.SaveChangesAsync();

        return await context.Industries.ToArrayAsync();
    }

    private async static Task<IssuerEntity[]> SeedIssuersAsync(InvestorDbContext context, CountryEntity[] countries, IndustryEntity[] industries)
    {
        await context.Issuers.AddRangeAsync(
            new IssuerEntity(countries[0], industries[4], "МТС"),
            new IssuerEntity(countries[0], industries[3], "Сургутнефтегаз"),
            new IssuerEntity(countries[0], industries[2], "Алроса"),
            new IssuerEntity(countries[1], industries[0], "Apple"),
            new IssuerEntity(countries[0], industries[5], "Яндекс"),
            new IssuerEntity(countries[0], industries[1], "Сбербанк России"));

        await context.SaveChangesAsync();

        return await context.Issuers.ToArrayAsync();
    }

    private async static Task<AssetEntity[]> SeedAssetsAsync(InvestorDbContext context, IssuerEntity[] issuers, StockExchangeEntity[] stockExchanges)
    {
        var circulations = new CirculationPeriodEntity[]
        {
            new(new DateTime(2016, 7, 16)),
            new(new DateTime(2004, 2, 11)),
            new(new DateTime(2005, 1, 11)),
            new(new DateTime(2011, 11, 29)),
            new(new DateTime(2020, 9, 8)),
            new(new DateTime(2014, 6, 4)),
            new(new DateTime(2007, 7, 20)),
            new(new DateTime(2020, 1, 20)),
            new(new DateTime(2019, 6, 19))
        };

        await context.Assets.AddRangeAsync(
            new ShareEntity("Сбербанк России, акция привелегированная", circulations[0], "RUB", "RU0009029557", issuers[5], 10, stockExchanges[0], "SBERP", true),
            new ShareEntity("МТС, акция обыкновенная", circulations[1], "RUB", "RU0007775219", issuers[0], 10, stockExchanges[0], "MTSS", false),
            new ShareEntity("Сургутнефтегаз, акция обыкновенная", circulations[2], "RUB", "RU0008926258", issuers[1], 100, stockExchanges[0], "SNGS", false),
            new ShareEntity("Алроса, акция обыкновенная", circulations[3], "RUB", "RU0007252813", issuers[2], 10, stockExchanges[0], "ALRS", false),
            new ShareEntity("Apple, акция обыкновенная", circulations[4], "USD", "US0378331005", issuers[3], 1, stockExchanges[0], "AAPL-RM", false),
            new ShareEntity("ЯНДЕКС Н.В., акция обыкновенная", circulations[5], "EUR", "NL0009805522", issuers[4], 1, stockExchanges[0], "YNDX", false),
            new ShareEntity("Сбербанк России, акция обыкновенная", circulations[6], "RUB", "RU0009029540", issuers[5], 10, stockExchanges[0], "SBER", false),
            new BondEntity("Сбербанк ПАО 001Р-SBER15", circulations[7], "RUB", "RU000A101C89", issuers[5], 1, stockExchanges[0], "RU000A101C89", BondType.Corporate),
            new BondEntity("СберИОС 001Р-177R GMKN 100", circulations[8], "RUB", "RU000A1013J4", issuers[5], 1, stockExchanges[0], "RU000A1013J4", BondType.Corporate));

        await context.SaveChangesAsync();

        return await context.Assets.ToArrayAsync();
    }
}