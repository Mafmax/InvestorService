namespace Mafmax.InvestorService.Services.Services.Queries.Assets;

/// <summary>
/// Query to find assets
/// </summary>
public record FindAssetsWithClassQuery(string SearchString,
        string AssetsClass,
        int MinimalSearchStringLength)
    : FindAssetsQuery(SearchString, MinimalSearchStringLength);