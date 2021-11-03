namespace Mafmax.InvestorService.Model.Entities.Assets
{

    /// <summary>
    /// Bond entity
    /// </summary>
    public class BondEntity : AssetEntity
    {

        /// <summary>
        /// Type of bond.
        /// </summary>
        public BondType Type { get; set; }
    }
}
