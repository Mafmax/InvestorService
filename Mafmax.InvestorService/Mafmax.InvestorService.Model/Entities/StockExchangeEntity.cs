using System.ComponentModel.DataAnnotations;

namespace Mafmax.InvestorService.Model.Entities
{

    /// <summary>
    /// Stock exchange entity
    /// </summary>
    public class StockExchangeEntity
    {

        /// <summary>
        /// Identifier
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Stock exchange key e.g. MOEX
        /// </summary>
        [Required]
        public string Key { get; set; } = null!;

        /// <summary>
        /// Stock exchange name
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;
    }
}
