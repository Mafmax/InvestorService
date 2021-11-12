using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Services.Queries.Interfaces;

namespace Mafmax.InvestorService.Services.Services.Queries.Assets;

/// <summary>
/// Query to find assets
/// </summary>
public record FindAssetsQuery(string SearchString, int MinimalSearchStringLength) 
    : IQuery<ShortAssetDto[]>;