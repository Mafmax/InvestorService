using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Services.Queries.Interfaces;

namespace Mafmax.InvestorService.Services.Services.Queries.Assets;

/// <summary>
/// Query to get all issuer assets
/// </summary>
public record GetIssuerAssetsQuery(int IssuerId)
    : IQuery<ShortAssetDto[]>;