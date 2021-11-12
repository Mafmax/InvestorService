namespace Mafmax.InvestorService.Services.DTOs;

/// <summary>
/// Short info DTO for <see cref="Mafmax.InvestorService.Model.Entities.Assets.AssetEntity"/>
/// </summary>
public record ShortAssetDto(int Id,
    string Name,
    string Class,
    string Isin,
    string Ticker,
    string ExchangeStockCode,
    string IssuerName);