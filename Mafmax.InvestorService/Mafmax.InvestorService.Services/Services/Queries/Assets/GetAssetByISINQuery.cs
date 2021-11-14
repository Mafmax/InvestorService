using Mafmax.InvestorService.Services.DTOs;
using MediatR;

namespace Mafmax.InvestorService.Services.Services.Queries.Assets;

/// <summary>
/// Query to get asset by ISIN
/// </summary>
public record GetAssetByIsinQuery(string Isin) : IRequest<AssetDto?>;