using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Mafmax.InvestorService.Model.Context;
using Mafmax.InvestorService.Model.Entities;
using Mafmax.InvestorService.Model.Entities.Assets;
using Mafmax.InvestorService.Model.Extensions;
using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Exceptions;
using Mafmax.InvestorService.Services.Services.Queries.Assets;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Mafmax.InvestorService.Services.Exceptions.Helpers.ThrowsHelper;

namespace Mafmax.InvestorService.Services.Services.Queries.Handlers;

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
    private static void CheckSearchString(FindAssetsQuery query)
    {
        if (query.SearchString.Length < query.MinimalSearchStringLength)
            throw new InvalidOperationException(
                $"SearchString length must be greater than {query.MinimalSearchStringLength}");
    }

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
    /// <see cref="InvalidOperationException"/>
    public async Task<ShortAssetDto[]> Handle(FindAssetsQuery query, CancellationToken token)
    {
        CheckSearchString(query);
        return await Db.Assets
            .Include(x => x.Issuer)
            .Where(AssetEntity.Specs.Search(query.SearchString))
            .Select(x => Mapper.Map<ShortAssetDto>(x))
            .ToArrayAsync(token);
    }

    /// <summary>
    /// Finds assets
    /// </summary>
    /// <returns>Array of assets found</returns>
    /// <exception cref="InvalidOperationException"/>
    public async Task<ShortAssetDto[]> Handle(FindAssetsWithClassQuery query, CancellationToken token)
    {
        CheckSearchString(query);
        return await Db.Assets
            .Include(x => x.Issuer)
            .Where(AssetEntity.Specs.Search(query.SearchString, query.AssetsClass))
            .Select(x => Mapper.Map<ShortAssetDto>(x))
            .ToArrayAsync(token);
    }

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
            .Select(x => Mapper.Map<ShortAssetDto>(x))
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