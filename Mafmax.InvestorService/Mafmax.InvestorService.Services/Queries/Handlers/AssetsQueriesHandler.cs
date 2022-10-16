using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Mafmax.InvestorService.Model.Context;
using Mafmax.InvestorService.Model.Entities;
using Mafmax.InvestorService.Model.Entities.Assets;
using Mafmax.InvestorService.Model.Extensions;
using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Exceptions;
using Mafmax.InvestorService.Services.Queries.Assets;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Mafmax.InvestorService.Services.Exceptions.Helpers.ThrowsHelper;

namespace Mafmax.InvestorService.Services.Queries.Handlers;

/// <summary>
/// Handle queries associated with assets
/// </summary>
public class AssetsQueriesHandler : ServiceBase<InvestorDbContext>,
    IRequestHandler<FindAssetsQuery, ShortAssetDto[]>,
    IRequestHandler<FindAssetsWithClassQuery, ShortAssetDto[]>,
    IRequestHandler<GetAssetByIsinQuery, AssetDto?>,
    IRequestHandler<GetAssetByIdQuery, AssetDto?>,
    IRequestHandler<GetIssuerAssetsQuery, ShortAssetDto[]>
{
    private IQueryable<AssetEntity> FullAssetWithIncludes => Db.Assets
        .Include(x => x.Stock)
        .Include(x => x.Circulation)
        .Include(x => x.Issuer).ThenInclude(x => x.Country)
        .Include(x => x.Issuer).ThenInclude(x => x.Industry);

    /// <inheritdoc />
    public AssetsQueriesHandler(InvestorDbContext db, IMapper mapper) : base(db, mapper) { }

    /// <summary>
    /// Finds assets
    /// </summary>
    /// <returns>Array of assets found</returns>
    public async Task<ShortAssetDto[]> Handle(FindAssetsQuery query, CancellationToken token) =>
        await Db.Assets
            .Include(x => x.Issuer)
            .Where(AssetEntity.Specs.Search(query.SearchString))
            .ProjectTo<ShortAssetDto>(Mapper.ConfigurationProvider)
            .ToArrayAsync(token);

    /// <summary>
    /// Finds assets
    /// </summary>
    /// <returns>Array of assets found</returns>
    public async Task<ShortAssetDto[]> Handle(FindAssetsWithClassQuery query, CancellationToken token) =>
        await Db.Assets
            .Include(x => x.Issuer)
            .Where(AssetEntity.Specs.Search(query.SearchString, query.AssetsClass))
            .ProjectTo<ShortAssetDto>(Mapper.ConfigurationProvider)
            .ToArrayAsync(token);

    /// <summary>
    /// Gets asset
    /// </summary>
    /// <returns>Asset</returns>
    ///<exception cref="EntityNotFoundException"/>
    public async Task<AssetDto?> Handle(GetAssetByIsinQuery query, CancellationToken token)
    {
        var asset = await FullAssetWithIncludes
            .FirstOrDefaultAsync(x => x.Isin.Equals(query.Isin), token);

        if (asset is null) ThrowEntityNotFound<AssetEntity>(query.Isin);

        return Mapper.Map<AssetDto>(asset);
    }

    /// <summary>
    /// Gets all issuer assets
    /// </summary>
    /// <returns>Array of issuer assets</returns>
    /// <exception cref="EntityNotFoundException"/>
    public async Task<ShortAssetDto[]> Handle(GetIssuerAssetsQuery query, CancellationToken token)
    {
        var issuer = await Db.Issuers.ByIdAsync(query.IssuerId, token);

        if (issuer is null) ThrowEntityNotFound<IssuerEntity>(query.IssuerId);

        return await Db.Assets
            .Include(x => x.Stock)
            .Include(x => x.Issuer)
            .Where(AssetEntity.Specs.ByIssuerValidOnly(query.IssuerId))
            .OrderBy(x => x.Circulation.Start)
            .ProjectTo<ShortAssetDto>(Mapper.ConfigurationProvider)
            .ToArrayAsync(token);

    }

    /// <summary>
    /// Gets asset by id
    /// </summary>
    /// <returns>Asset</returns>
    /// <exception cref="EntityNotFoundException"/>
    public async Task<AssetDto?> Handle(GetAssetByIdQuery query, CancellationToken token)
    {
        var asset = await FullAssetWithIncludes
            .ByIdAsync(query.Id, token);

        if (asset is null) ThrowEntityNotFound<AssetEntity>(query.Id);

        return Mapper.Map<AssetDto>(asset);
    }
}