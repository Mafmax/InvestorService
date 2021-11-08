using Mafmax.InvestorService.Model.Entities.ExchangeTransaction;

namespace Mafmax.InvestorService.Services.DTOs
{

    /// <summary>
    /// DTO for <see cref="Mafmax.InvestorService.Model.Entities.ExchangeTransaction.ExchangeTransactionEntity"/>
    /// </summary>
    public record ExchangeTransactionDto(int Id,
        decimal Price, 
        AssetDto Asset,
        ExchangeTransactionType Type,
        int LotsCount);
}