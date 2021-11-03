using System.ComponentModel.DataAnnotations;

namespace Mafmax.InvestorService.Model.Entities
{

    /// <summary>
    /// Issuer entity
    /// </summary>
    public class IssuerEntity
    {

        /// <summary>
        /// Identifier
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Company name
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Company country
        /// </summary>
        public CountryEntity Country { get; set; } = null!;

        /// <summary>
        /// Company industry
        /// </summary>
        public IndustryEntity Industry { get; set; } = null!;
    }
}
