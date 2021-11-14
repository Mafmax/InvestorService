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

    private static IMediator GetMediator(Assembly assembly,Guid token)
    {
        IServiceCollection s = new ServiceCollection();

        s.AddScoped(_=>GetContext(token));

        s.AddRequestHandlers();

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
            new() { Name = "Country1"},
            new() { Name = "Country2"}
        };

    private static IndustryEntity[] GetIndustries() =>
        new IndustryEntity[]
        {
            new(){Name = "Industry1"},
            new(){Name = "Industry2"},
            new(){Name = "Industry3"},
            new(){Name = "Industry4"},
        };

    private static IssuerEntity[] GetIssuers(CountryEntity[] countries, IndustryEntity[] industries) =>
        new IssuerEntity[]
        {
            new() {Industry = industries[1], Country = countries[1],Name = "Issuer1"},
            new() {Industry = industries[2], Country = countries[0],Name = "Issuer2"},
            new() {Industry = industries[2], Country = countries[1],Name = "Issuer3"},
            new() {Industry = industries[3], Country = countries[0],Name = "Issuer4"}
        };

    private static StockExchangeEntity[] GetStocks() =>
        new StockExchangeEntity[]
        {
            new () {Key = "Key1", Name = "Stock1"},
            new () {Key = "Key2", Name = "Stock2"}
        };

    private static AssetEntity[] GetAssets(IssuerEntity[] issuers, StockExchangeEntity[] stocks) =>
        new AssetEntity[]
        {
            new BondEntity(){Name = "Bond1",Circulation = new(){Start = new (2020,4,10),},Currency = "GPU",Isin="ISIN1",Issuer = issuers[1],LotSize = 1, Stock = stocks[0],Ticker = "Ticker1",Type = BondType.Corporate},
            new BondEntity(){Name = "Bond2",Circulation = new(){Start = new (2006,5,11),End = new(2015,6,13)},Currency = "USB",Isin="ISIN2",Issuer = issuers[0],LotSize = 1, Stock = stocks[1],Ticker = "Ticker2",Type = BondType.Municipal},
            new BondEntity(){Name = "Bond3",Circulation = new(){Start = new (2022,4,15)},Currency = "ADD",Isin="ISIN3",Issuer = issuers[0],LotSize = 1, Stock = stocks[1],Ticker = "Ticker3",Type = BondType.Government},
            new BondEntity(){Name = "Bond4",Circulation = new(){Start = new (2013,2,22), End = new(2018,11,1)},Currency = "MOQ",Isin="ISIN4",Issuer = issuers[1],LotSize = 1, Stock = stocks[1],Ticker = "Ticker4",Type = BondType.Corporate},
            new BondEntity(){Name = "Bond5",Circulation = new(){Start = new (2020,6,21)},Currency = "ENG",Isin="ISIN5",Issuer = issuers[2],LotSize = 1, Stock = stocks[0],Ticker = "Ticker5",Type = BondType.Corporate},
            new ShareEntity(){Name = "Share1",Circulation = new(){Start = new (2002,7,22)},Currency = "SSD",Isin="ISIN6",Issuer = issuers[1],LotSize = 1, Stock = stocks[1],Ticker = "Ticker6",IsPreferred = true},
            new ShareEntity(){Name = "Share2",Circulation = new(){Start = new (2021,1, 22)},Currency = "TDD",Isin="ISIN7",Issuer = issuers[2],LotSize = 1, Stock = stocks[1],Ticker = "Ticker7",IsPreferred = false},
            new ShareEntity(){Name = "Share3",Circulation = new(){Start = new (1998,1,1), End = new (2001,3,1)},Currency = "ABC",Isin="ISIN8",Issuer = issuers[1],LotSize = 1, Stock = stocks[0],Ticker = "Ticker8",IsPreferred = false},
            new ShareEntity(){Name = "Share4",Circulation = new(){Start = new (2007,7,12)},Currency = "FIN",Isin="ISIN9",Issuer = issuers[2],LotSize = 1, Stock = stocks[0],Ticker = "Ticker9",IsPreferred = true},
        };

    private static ExchangeTransactionEntity[] GetTransactions(AssetEntity[] assets) =>
        new ExchangeTransactionEntity[]
        {
            new() {Asset = assets[8], LotsCount = 5, OneLotPrice = 190.40m, Type = ExchangeTransactionType.Buy},
            new() {Asset = assets[0], LotsCount = 1, OneLotPrice = 20.03m, Type = ExchangeTransactionType.Sell},
            new() {Asset = assets[2], LotsCount = 12, OneLotPrice = 60.00m, Type = ExchangeTransactionType.Buy},
            new() {Asset = assets[1], LotsCount = 1, OneLotPrice = 65.00m, Type = ExchangeTransactionType.Buy},

            new() {Asset = assets[3], LotsCount = 4500, OneLotPrice = 990.00m, Type = ExchangeTransactionType.Sell},
            new() {Asset = assets[5], LotsCount = 5, OneLotPrice = 120.00m, Type = ExchangeTransactionType.Buy},
            new() {Asset = assets[4], LotsCount = 150, OneLotPrice = 3.00m, Type = ExchangeTransactionType.Sell},
            new() {Asset = assets[5], LotsCount = 5, OneLotPrice = 6.00m, Type = ExchangeTransactionType.Buy},

            new() {Asset = assets[6], LotsCount = 25, OneLotPrice = 710.00m, Type = ExchangeTransactionType.Sell},
            new() {Asset = assets[7], LotsCount = 15, OneLotPrice = 18.00m, Type = ExchangeTransactionType.Sell},
            new() {Asset = assets[2], LotsCount = 5, OneLotPrice = 243.00m, Type = ExchangeTransactionType.Sell},
            new() {Asset = assets[8], LotsCount = 1, OneLotPrice = 106.05m, Type = ExchangeTransactionType.Sell},
        };

    private static InvestmentPortfolioEntity[] GetPortfolios(ExchangeTransactionEntity[] transactions) =>
        new InvestmentPortfolioEntity[]
        {
            new()
            {
                Name = "Portfolio1", TargetDescription = "SomeTargetDescription1",
                Transactions = new(transactions[..3])
            },
            new()
            {
                Name = "Portfolio2", TargetDescription = "SomeTargetDescription2",
                Transactions = new(transactions[3..5])
            },
            new()
            {
                Name = "Portfolio3", TargetDescription = "SomeTargetDescription3",
                Transactions = new(transactions[5..10])
            },
            new()
            {
                Name = "Portfolio4", TargetDescription = "SomeTargetDescription4",
                Transactions = new(transactions[10..12])
            },
        };

    private static UserEntity[] GetUsers(InvestmentPortfolioEntity[] portfolios) =>
        new UserEntity[]
        {
            new InvestorEntity()
                {Login = "Investor1", PasswordHash = SHA256.HashData(Encoding.Default.GetBytes("12345")), Portfolios = new(portfolios[..3])},
            new InvestorEntity()
                {Login = "Investor2", PasswordHash = SHA256.HashData(Encoding.Default.GetBytes("12345")), Portfolios = new(portfolios[3..4])},
            new InvestorEntity()
                {Login = "Investor3", PasswordHash = SHA256.HashData(Encoding.Default.GetBytes("12345"))}
        };
}