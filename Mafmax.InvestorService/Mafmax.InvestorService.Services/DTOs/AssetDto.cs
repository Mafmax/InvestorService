namespace Mafmax.InvestorService.Services.DTOs;

/// <summary>
/// Full information DTO for <see cref="Mafmax.InvestorService.Model.Entities.Assets.AssetEntity"/>
/// </summary>
public record AssetDto(int Id,
    string Ticker,
    string Isin,
    string Name,
    IssuerDto Issuer,
    StockExchangeDto Stock,
    CirculationPeriodDto Circulation,
    string Currency,
    int LotSize,
    string Class);