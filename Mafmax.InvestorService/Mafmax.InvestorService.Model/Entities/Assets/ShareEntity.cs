// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Mafmax.InvestorService.Model.Entities.Assets;

/// <summary>
/// Share (stock) entity
/// </summary>
public class ShareEntity : AssetEntity
{
    /// <inheritdoc />
    protected ShareEntity()
    {
    }

    /// <inheritdoc />
    public ShareEntity(string name, CirculationPeriodEntity circulation, string currency, string isin, IssuerEntity issuer, int lotSize, StockExchangeEntity stock, string ticker,  bool isPreferred)
    {
        Name = name;
        Circulation = circulation;
        Currency = currency;
        Isin = isin;
        Issuer = issuer;
        LotSize = lotSize;
        Stock = stock;
        Ticker = ticker;
        IsPreferred = isPreferred;
    }

    /// <summary>
    /// Flag of preferred or common share
    /// </summary>
    public bool IsPreferred { get; protected set; }
}