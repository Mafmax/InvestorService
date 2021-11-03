using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mafmax.InvestorService.Model.Entities.Assets
{

    /// <summary>
    /// Base type for assets
    /// </summary>
    [Table("Assets")]
    public abstract class AssetEntity
    {

        /// <summary>
        /// Unique key for asset
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Alphabetic asset identifier
        /// </summary>
        [Required]
        public string Ticker { get; set; } = null!;

        /// <summary>
        /// International Securities Identification Number
        /// </summary>
        [Required]
        public string Isin { get; set; } = null!;

        /// <summary>
        /// Asset name
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Assets issuer company
        /// </summary>
        public IssuerEntity Issuer { get; set; } = null!;

        /// <summary>
        /// Stock exchange organization
        /// </summary>
        public StockExchangeEntity Stock { get; set; } = null!;

        /// <summary>
        /// Period of assets circulation
        /// </summary>
        public CirculationPeriodEntity Circulation { get; set; } = null!;

        /// <summary>
        /// Currency name e.g. USD, RUB
        /// </summary>
        [Column("BaseCurrency")]
        public string Currency { get; set; } = null!;

        /// <summary>
        /// Number of assets in one lot 
        /// </summary>
        [Required]
        public int LotSize { get; set; }

        /// <summary>
        /// Asset class e.g. share or bond
        /// </summary>
        [NotMapped]
        public string Class { get; } = null!;
    }
}
