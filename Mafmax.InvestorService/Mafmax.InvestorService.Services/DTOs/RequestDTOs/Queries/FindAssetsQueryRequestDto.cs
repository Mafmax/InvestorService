using Mafmax.InvestorService.Services.Services.Queries.Assets;

namespace Mafmax.InvestorService.Services.DTOs.RequestDTOs.Queries
{
    /// <summary>
    /// DTO to receive user data from request for <see cref="FindAssetsQuery"/>
    /// </summary>
    public record FindAssetsQueryRequestDto(string SearchString)
    {

        /// <summary>
        /// Creates query (<inheritdoc cref="FindAssetsQuery"/>) from DTO
        /// </summary>
        /// <returns>Instance of <see cref="FindAssetsQuery"/></returns>
        public FindAssetsQuery GetQuery(int minimumSearchStringLength)
        {
            return new(SearchString, minimumSearchStringLength);
        }

        /// <summary>
        /// Creates query (<inheritdoc cref="FindAssetsWithClassQuery"/>) from DTO
        /// </summary>
        /// <returns>Instance of <see cref="FindAssetsWithClassQuery"/></returns>
        public FindAssetsWithClassQuery GetQuery(int minimumSearchStringLength, string assetsClass)
        {
            return new(SearchString, assetsClass, minimumSearchStringLength);
        }
    }
}
