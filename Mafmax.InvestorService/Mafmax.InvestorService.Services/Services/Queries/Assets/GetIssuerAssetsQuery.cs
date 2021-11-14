using Mafmax.InvestorService.Services.DTOs;
using MediatR;

namespace Mafmax.InvestorService.Services.Services.Queries.Assets;

/// <summary>
/// Query to get all issuer assets
/// </summary>
public record GetIssuerAssetsQuery(int IssuerId)
    : IRequest<ShortAssetDto[]>;