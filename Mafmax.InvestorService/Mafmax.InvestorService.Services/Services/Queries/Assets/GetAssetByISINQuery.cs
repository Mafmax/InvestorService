using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Services.Queries.Interfaces;

namespace Mafmax.InvestorService.Services.Services.Queries.Assets;

/// <summary>
/// Query to get asset by ISIN
/// </summary>
public record GetAssetByIsinQuery(string Isin) : IQuery<AssetDto?>;