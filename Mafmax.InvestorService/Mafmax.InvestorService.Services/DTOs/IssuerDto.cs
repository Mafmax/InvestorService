namespace Mafmax.InvestorService.Services.DTOs
{
    /// <summary>
    /// DTO for <see cref="Mafmax.InvestorService.Model.Entities.IssuerEntity"/>
    /// </summary>
    public record IssuerDto(int Id, 
        string Name, 
        string Country, 
        string Industry);
}
