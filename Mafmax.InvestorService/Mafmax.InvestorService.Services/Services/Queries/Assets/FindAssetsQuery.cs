using Mafmax.InvestorService.Services.DTOs;
using MediatR;

namespace Mafmax.InvestorService.Services.Services.Queries.Assets;

/// <summary>
/// Query to find assets
/// </summary>
public record FindAssetsQuery(string SearchString, int MinimalSearchStringLength) 
    : IRequest<ShortAssetDto[]>;