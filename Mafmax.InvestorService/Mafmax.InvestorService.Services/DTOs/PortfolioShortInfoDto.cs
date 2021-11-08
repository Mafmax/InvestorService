namespace Mafmax.InvestorService.Services.DTOs
{

    /// <summary>
    /// DTO for short info of <see cref="Mafmax.InvestorService.Model.Entities.InvestmentPortfolioEntity"/>
    /// </summary>
    public record PortfolioShortInfoDto(int Id, string Name, string TargetDescription);
}
