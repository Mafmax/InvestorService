using System.ComponentModel.DataAnnotations;

namespace Mafmax.InvestorService.Model.Entities
{

    /// <summary>
    /// Country entity
    /// </summary>
    public class CountryEntity
    {

        /// <summary>
        /// Identifier
        /// </summary>
        [Key]
        public int Id { get; set; } 

        /// <summary>
        /// Country name
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;
    }
}
