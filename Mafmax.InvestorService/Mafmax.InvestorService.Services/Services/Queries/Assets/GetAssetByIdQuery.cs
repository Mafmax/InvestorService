using Mafmax.InvestorService.Services.DTOs;
using MediatR;

namespace Mafmax.InvestorService.Services.Services.Queries.Assets;

/// <summary>
/// Query to get asset by id
/// </summary>
public record GetAssetByIdQuery(int Id) : IRequest<AssetDto?>;