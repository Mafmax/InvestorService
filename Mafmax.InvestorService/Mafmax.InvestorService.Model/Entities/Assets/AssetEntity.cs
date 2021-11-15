using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinqSpecs.Core;
using Mafmax.InvestorService.Model.Interfaces;
using Mafmax.InvestorService.Model.Specifications.Assets;

// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Mafmax.InvestorService.Model.Entities.Assets;

/// <summary>
/// Base type for assets
/// </summary>
[Table("Assets")]
public abstract class AssetEntity : IHasId<int>
{

    /// <summary>
    /// Specifications for <see cref="AssetEntity"/> and derived
    /// </summary>
    public static class Specs
    {

        /// <summary>
        /// Search assets specification
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public static Specification<AssetEntity> Search(string searchString, StringComparison searchType = StringComparison.OrdinalIgnoreCase) =>
            new SearchByIsinSpecification(searchString, searchType)
            || new SearchByNameSpecification(searchString, searchType)
            || new SearchByTickerSpecification(searchString, searchType);

        /// <summary>
        /// Search assets specification
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="className">Assets class name to search</param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public static Specification<AssetEntity> Search(string searchString, string className, StringComparison searchType = StringComparison.OrdinalIgnoreCase) =>
            Search(searchString, searchType)
            && new InClassSpecification(className, searchType);

        /// <summary>
        /// Search valid issuer assets specification
        /// </summary>
        /// <param name="issuerId"></param>
        /// <returns></returns>
        public static Specification<AssetEntity> ByIssuerValidOnly(int issuerId) =>
            new ByIssuerSpecification(issuerId)
            && new IsValidSpecification();
    }

    /// <summary>
    /// Unique key for asset
    /// </summary>
    [Key]
    public int Id { get; protected set; }

    /// <summary>
    /// Alphabetic asset identifier
    /// </summary>
    public string Ticker { get; protected set; } = string.Empty;

    /// <summary>
    /// International Securities Identification Number
    /// </summary>
    public string Isin { get; protected set; } = string.Empty;

    /// <summary>
    /// Asset name
    /// </summary>
    public string Name { get; protected set; } = string.Empty;

    /// <summary>
    /// Assets issuer company
    /// </summary>
    public IssuerEntity Issuer { get; protected set; } = null!;

    /// <summary>
    /// Stock exchange organization
    /// </summary>
    public StockExchangeEntity Stock { get; protected set; } = null!;

    /// <summary>
    /// Period of assets circulation
    /// </summary>
    public CirculationPeriodEntity Circulation { get; protected set; } = null!;

    /// <summary>
    /// Currency name e.g. USD, RUB
    /// </summary>
    [Column("BaseCurrency")]
    public string Currency { get; protected set; } = string.Empty;

    /// <summary>
    /// Number of assets in one lot 
    /// </summary>
    public int LotSize { get; protected set; }

    /// <summary>
    /// Asset class e.g. share or bond
    /// </summary>
    [NotMapped]
    public string Class { get; } = null!;
}