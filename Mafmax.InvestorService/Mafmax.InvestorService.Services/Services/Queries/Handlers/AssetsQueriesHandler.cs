using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Mafmax.InvestorService.Model.Context;
using Mafmax.InvestorService.Model.Entities;
using Mafmax.InvestorService.Model.Entities.Assets;
using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Exceptions;
using Mafmax.InvestorService.Services.Services.Queries.Assets;
using Mafmax.InvestorService.Services.Services.Queries.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Mafmax.InvestorService.Services.Exceptions.Helpers.ThrowsHelper;

namespace Mafmax.InvestorService.Services.Services.Queries.Handlers
{
    /// <summary>
    /// Handle queries associated with assets
    /// </summary>
    public class AssetsQueriesHandler : ServiceBase<InvestorDbContext>,
        IQueryHandler<FindAssetsQuery, ShortAssetDto[]>,
        IQueryHandler<FindAssetsWithClassQuery, ShortAssetDto[]>,
        IQueryHandler<GetAssetByIsinQuery, AssetDto?>,
        IQueryHandler<GetAssetByIdQuery, AssetDto?>,
        IQueryHandler<GetIssuerAssetsQuery, ShortAssetDto[]>
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

        private IQueryable<AssetEntity> Find(string searchString)
        {
            Expression<Func<AssetEntity, bool>> searchPredicateExpression = x =>
                x.Name.Contains(searchString) || x.Ticker.Contains(searchString) || x.Isin.Contains(searchString);

            return Db.Assets.Where(searchPredicateExpression).Include(x => x.Issuer);
        }


        /// <inheritdoc />
        public AssetsQueriesHandler(InvestorDbContext db, IMapper mapper) : base(db, mapper) { }

        /// <summary>
        /// Finds assets
        /// </summary>
        /// <returns>Array of assets found</returns>
        /// <see cref="InvalidOperationException"/>
        public async Task<ShortAssetDto[]> AskAsync(FindAssetsQuery query)
        {
            CheckSearchString(query);
            return await Find(query.SearchString)
                .Select(x => Mapper.Map<ShortAssetDto>(x))
                .ToArrayAsync();
        }

        /// <summary>
        /// Finds assets
        /// </summary>
        /// <returns>Array of assets found</returns>
        /// <exception cref="InvalidOperationException"/>
        public async Task<ShortAssetDto[]> AskAsync(FindAssetsWithClassQuery query)
        {
            CheckSearchString(query);
            return await Find(query.SearchString)
                .Where(x => x.Class.Equals(query.AssetsClass, StringComparison.OrdinalIgnoreCase))
                .Select(x => Mapper.Map<ShortAssetDto>(x))
                .ToArrayAsync();
        }

        /// <summary>
        /// Gets asset
        /// </summary>
        /// <returns>Asset</returns>
        ///<exception cref="EntityNotFoundException"/>
        public async Task<AssetDto?> AskAsync(GetAssetByIsinQuery query)
        {
            var asset = await FullAssetWithIncludes
                .FirstOrDefaultAsync(x => x.Isin == query.Isin);

            if (asset is null) ThrowEntityNotFound<AssetEntity>(query.Isin);

            return Mapper.Map<AssetDto>(asset);
        }

        /// <summary>
        /// Gets all issuer assets
        /// </summary>
        /// <returns>Array of issuer assets</returns>
        /// <exception cref="EntityNotFoundException"/>
        public async Task<ShortAssetDto[]> AskAsync(GetIssuerAssetsQuery query)
        {
            var issuer = await Db.Issuers.FindAsync(query.IssuerId);

            if (issuer is null) ThrowEntityNotFound<IssuerEntity>(query.IssuerId);

            return await Db.Assets
                .Include(x => x.Stock)
                .Include(x => x.Issuer)
                .Where(x => x.Issuer.Id == query.IssuerId)
                .OrderBy(x => x.Circulation.Start)
                .Select(x => Mapper.Map<ShortAssetDto>(x))
                .ToArrayAsync();
        }

        /// <summary>
        /// Gets asset by id
        /// </summary>
        /// <returns>Asset</returns>
        /// <exception cref="EntityNotFoundException"/>
        public async Task<AssetDto?> AskAsync(GetAssetByIdQuery query)
        {
            var asset = await FullAssetWithIncludes
                .FirstOrDefaultAsync(x => x.Id.Equals(query.Id));

            if (asset is null) ThrowEntityNotFound<AssetEntity>(query.Id);
            
            return Mapper.Map<AssetDto>(asset);
        }
    }
}
