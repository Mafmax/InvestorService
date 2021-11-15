// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
namespace Mafmax.InvestorService.Model.Entities.Assets;

/// <summary>
/// Bond entity
/// </summary>
public class BondEntity : AssetEntity
{
    /// <inheritdoc />
    // ReSharper disable once UnusedMember.Global
    protected BondEntity()
    {
    }

    /// <inheritdoc />
  /*  public BondEntity(string name, CirculationPeriodEntity circulation, string currency, string isin, IssuerEntity issuer, int lotSize, StockExchangeEntity stock, string ticker, BondType type)
    {
        Name = name;
        Circulation = circulation;
        Currency = currency;
        Isin = isin;
        Issuer = issuer;
        LotSize = lotSize;
        Stock = stock;
        Ticker = ticker;
        Type = type;
    }*/

    public BondEntity(string name, CirculationPeriodEntity circulation, string currency, string isin, IssuerEntity issuer, int lotSize, StockExchangeEntity stock, string ticker,    BondType type   )
    {
        Name = name;
        Circulation = circulation;
        Currency = currency;
        Isin = isin;
        Issuer = issuer;
        LotSize = lotSize;
        Stock = stock;
        Ticker = ticker;
        Type = type;
    }

    /// <summary>
    /// Type of bond.
    /// </summary>
    public BondType Type { get; protected set; }
}