using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mafmax.InvestorService.Model.Entities.ExchangeTransaction;

namespace Mafmax.InvestorService.Model.Entities
{

    /// <summary>
    /// Portfolio with transactions
    /// </summary>
    public class InvestmentPortfolioEntity
    {

        /// <summary>
        /// Identifier
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Display name
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Description of portfolio goal
        /// </summary>
        [MinLength(10)]
        [MaxLength(500)]
        [Required]
        public string TargetDescription { get; set; } = null!;

        /// <summary>
        /// Collection of <inheritdoc cref="ExchangeTransactionEntity"/>
        /// </summary>
        public ICollection<ExchangeTransactionEntity> Transactions { get; set; } = new List<ExchangeTransactionEntity>();
    }
}
