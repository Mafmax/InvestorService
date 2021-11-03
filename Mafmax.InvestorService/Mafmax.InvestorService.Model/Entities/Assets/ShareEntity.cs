namespace Mafmax.InvestorService.Model.Entities.Assets
{

    /// <summary>
    /// Share (stock) entity
    /// </summary>
    public class ShareEntity : AssetEntity
    {

        /// <summary>
        /// Flag of preferred or common share
        /// </summary>
        public bool IsPreferred { get; set; }
    }
}
