using Mafmax.InvestorService.Model.Entities;
using Mafmax.InvestorService.Model.Entities.Assets;
using Mafmax.InvestorService.Model.Entities.ExchangeTransaction;
using Mafmax.InvestorService.Model.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Mafmax.InvestorService.Model.Context;

/// <summary>
/// Main db context
/// </summary>
public class InvestorDbContext : DbContext
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="options"></param>
    public InvestorDbContext(DbContextOptions<InvestorDbContext> options) : base(options) { }

    /// <summary>
    /// Asset entities
    /// </summary>!
    public DbSet<AssetEntity> Assets => Set<AssetEntity>();

    /// <summary>
    /// Investors, registered in service
    /// </summary>
    public DbSet<InvestorEntity> Investors => Set<InvestorEntity>();

    /// <summary>
    /// Users, registered in service
    /// </summary>
    public DbSet<UserEntity> Users => Set<UserEntity>();

    /// <summary>
    /// Issuers of assets
    /// </summary>
    public DbSet<IssuerEntity> Issuers => Set<IssuerEntity>();

    /// <summary>
    /// Platforms where assets are traded
    /// </summary>
    public DbSet<StockExchangeEntity> StockExchanges => Set<StockExchangeEntity>();

    /// <summary>
    /// Country entities
    /// </summary>
    public DbSet<CountryEntity> Countries => Set<CountryEntity>();

    /// <summary>
    /// Industry entities
    /// </summary>
    public DbSet<IndustryEntity> Industries => Set<IndustryEntity>();

    /// <summary>
    /// Portfolios with transactions
    /// </summary>
    public DbSet<InvestmentPortfolioEntity> InvestmentPortfolios => Set<InvestmentPortfolioEntity>();

    /// <summary>
    /// Transactions which investors was made
    /// </summary>
    public DbSet<ExchangeTransactionEntity> ExchangeTransactions => Set<ExchangeTransactionEntity>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder) => 
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InvestorDbContext).Assembly);
}