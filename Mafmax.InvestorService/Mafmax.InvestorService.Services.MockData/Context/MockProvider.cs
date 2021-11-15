using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Mafmax.InvestorService.Model.Context;
using Mafmax.InvestorService.Model.Entities;
using Mafmax.InvestorService.Model.Entities.Assets;
using Mafmax.InvestorService.Model.Entities.ExchangeTransaction;
using Mafmax.InvestorService.Model.Entities.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Mafmax.InvestorService.Services.Extensions;

namespace Mafmax.InvestorService.Services.MockData.Context;

public static class MockProvider
{

    private static readonly object Locker = new();

    public static IMediator GetMediator(Assembly assembly) =>
        GetMediator(assembly, Guid.NewGuid());

    private static IMediator GetMediator(Assembly assembly, Guid token)
    {
        IServiceCollection s = new ServiceCollection();

        s.AddScoped(_ => GetContext(token));

        s.AddRequestHandlers();

        s.ConfigureValidation();

        s.AddAutoMapper();

        s.AddMediatR(assembly);

        return s.BuildServiceProvider().GetRequiredService<IMediator>();
    }

    public static IMapper GetMapper(Assembly assembly)
    {
        var types = assembly.GetTypes().Where(x => x.BaseType == typeof(Profile));
        var mapper = new MapperConfiguration(cfg =>
        {
            foreach (var type in types)
                cfg.AddProfile(type);
        });
        return mapper.CreateMapper();
    }

    public static InvestorDbContext GetContext(string dbName)
    {
        var options =
        new DbContextOptionsBuilder<InvestorDbContext>()
            .UseInMemoryDatabase(dbName).Options;

        lock (Locker)
            return GetContext(options);
    }

    public static InvestorDbContext GetContext(Guid token) =>
        GetContext(token.ToString());

    private static InvestorDbContext GetContext(DbContextOptions<InvestorDbContext> options)
    {
        var db = new InvestorDbContext(options);
        if (db.Users.Any()) return db;

        var countries = GetCountries();
        var industries = GetIndustries();
        var issuers = GetIssuers(countries, industries);
        var stocks = GetStocks();
        var assets = GetAssets(issuers, stocks);
        var transactions = GetTransactions(assets);
        var portfolios = GetPortfolios(transactions);
        var users = GetUsers(portfolios);

        db.Countries.AddRange(countries);
        db.Industries.AddRange(industries);
        db.Issuers.AddRange(issuers);
        db.StockExchanges.AddRange(stocks);
        db.Assets.AddRange(assets);
        db.ExchangeTransactions.AddRange(transactions);
        db.InvestmentPortfolios.AddRange(portfolios);
        db.Users.AddRange(users);

        db.SaveChanges();
        return db;
    }
    private static CountryEntity[] GetCountries() =>
        new CountryEntity[]
        {
            new("Country1"),
            new("Country2")
        };

    private static IndustryEntity[] GetIndustries() =>
        new IndustryEntity[]
        {
            new("Industry1"),
            new("Industry2"),
            new("Industry3"),
            new("Industry4"),
        };

    private static IssuerEntity[] GetIssuers(CountryEntity[] countries, IndustryEntity[] industries) =>
        new IssuerEntity[]
        {
            new(countries[1], industries[1], "Issuer1"),
            new(countries[0], industries[2], "Issuer2"),
            new(countries[1], industries[2], "Issuer3"),
            new(countries[0], industries[3], "Issuer4")
        };

    private static StockExchangeEntity[] GetStocks() =>
        new StockExchangeEntity[]
        {
            new("Key1", "Stock1"),
            new("Key2", "Stock2")
        };

    private static AssetEntity[] GetAssets(IssuerEntity[] issuers, StockExchangeEntity[] stocks) =>
        new AssetEntity[]
        {
            new BondEntity("Bond1", new(new(2020, 4, 10)), "GPU", "RU024235JS29", issuers[1], 1, stocks[0], "Ticker1", BondType.Corporate),
            new BondEntity("Bond2", new(new(2006, 5, 11), new(2015, 6, 13)), "USB", "US9872349871", issuers[0], 1, stocks[1], "Ticker2", BondType.Municipal),
            new BondEntity("Bond3", new(new(2022, 4, 15)), "ADD", "RU2828282891", issuers[0], 1, stocks[1], "Ticker3", BondType.Government),
            new BondEntity("Bond4", new(new(2013, 2, 22), new(2018, 11, 1)), "MOQ", "RU0242323422", issuers[1], 1, stocks[1], "Ticker4", BondType.Corporate),
            new BondEntity("Bond5", new(new(2020, 6, 21)), "ENG", "RU6403729562", issuers[2], 1, stocks[0], "Ticker5", BondType.Corporate),
            new ShareEntity("Share1", new(new(2002, 7, 22)), "SSD", "RU6392659902", issuers[1], 1, stocks[1], "Ticker6", true),
            new ShareEntity("Share2", new(new(2021, 1, 22)), "TDD", "RU485920104", issuers[2], 1, stocks[1], "Ticker7", false),
            new ShareEntity("Share3", new(new(1998, 1, 1), new(2001, 3, 1)), "ABC", "RU0AS235JS29", issuers[1], 1, stocks[0], "Ticker8", false),
            new ShareEntity("Share4", new(new(2007, 7, 12)), "FIN", "RU7485926749", issuers[2], 1, stocks[0], "Ticker9", true),
        };

    private static ExchangeTransactionEntity[] GetTransactions(AssetEntity[] assets) =>
        new ExchangeTransactionEntity[]
        {
            new(assets[8], 5, 190.40m, ExchangeTransactionType.Buy),
            new(assets[0], 1, 20.03m, ExchangeTransactionType.Sell),
            new(assets[2], 12, 60.00m, ExchangeTransactionType.Buy),
            new(assets[1], 1, 65.00m, ExchangeTransactionType.Buy),

            new(assets[3], 4500, 990.00m, ExchangeTransactionType.Sell),
            new(assets[5], 5, 120.00m, ExchangeTransactionType.Buy),
            new(assets[4], 150, 3.00m, ExchangeTransactionType.Sell),
            new(assets[5], 5, 6.00m, ExchangeTransactionType.Buy),

            new(assets[6], 25, 710.00m, ExchangeTransactionType.Sell),
            new(assets[7], 15, 18.00m, ExchangeTransactionType.Sell),
            new(assets[2], 5, 243.00m, ExchangeTransactionType.Sell),
            new(assets[8], 1, 106.05m, ExchangeTransactionType.Sell),
        };

    private static InvestmentPortfolioEntity[] GetPortfolios(ExchangeTransactionEntity[] transactions) =>
        new InvestmentPortfolioEntity[]
        {
            new("Portfolio1", "SomeTargetDescription1", new(transactions[..3])),
            new("Portfolio2", "SomeTargetDescription2", new(transactions[3..5])),
            new("Portfolio3", "SomeTargetDescription3",
                new(transactions[5..10])),
            new("Portfolio4", "SomeTargetDescription4",
                new(transactions[10..12])),
        };

    private static UserEntity[] GetUsers(InvestmentPortfolioEntity[] portfolios) =>
        new UserEntity[]
        {
            new InvestorEntity("Investor1",
                SHA256.HashData(Encoding.Default.GetBytes("qpeQPE772")),
                new(portfolios[..3])),
            new InvestorEntity("Investor2",
                SHA256.HashData(Encoding.Default.GetBytes("56772oOoO")),
                new(portfolios[3..4])),
            new InvestorEntity("Investor3",
                SHA256.HashData(Encoding.Default.GetBytes("123kQkQPORT46")))
        };
}