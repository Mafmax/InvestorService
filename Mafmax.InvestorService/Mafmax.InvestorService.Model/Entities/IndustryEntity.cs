using System.ComponentModel.DataAnnotations;

namespace Mafmax.InvestorService.Model.Entities
{

    /// <summary>
    /// Company industry
    /// </summary>
    public class IndustryEntity
    {

        /// <summary>
        /// Industry identifier
        /// </summary>
        [Key]
        public int Id { get; set; } 

        /// <summary>
        /// Industry name
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;
    }
}
