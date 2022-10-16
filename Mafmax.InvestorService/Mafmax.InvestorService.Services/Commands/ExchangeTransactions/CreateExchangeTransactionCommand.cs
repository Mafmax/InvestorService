using FluentValidation;
using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Validation;
using MediatR;

namespace Mafmax.InvestorService.Services.Commands.ExchangeTransactions;

/// <summary>
/// Command to add transaction into portfolio
/// </summary>
public record CreateExchangeTransactionCommand(int InvestorId,
    int PortfolioId,
    int AssetId,
    bool OrderToBuy,
    decimal OneLotPrice,
    int LotsCount) : IRequest<ExchangeTransactionDto>
{
    
    // ReSharper disable once UnusedType.Global
    /// <inheritdoc />
    public class Validator : AbstractValidator<CreateExchangeTransactionCommand>
    {
        /// <inheritdoc />
        public Validator()
        {
            RuleFor(x => x.InvestorId)
                .SetValidator(new IdValidator<CreateExchangeTransactionCommand>("Investor Id"));

            RuleFor(x => x.PortfolioId)
                .SetValidator(new IdValidator<CreateExchangeTransactionCommand>("Portfolio Id"));

            RuleFor(x => x.AssetId)
                .SetValidator(new IdValidator<CreateExchangeTransactionCommand>("Asset Id"));

            RuleFor(x => x.OneLotPrice)
                .GreaterThan(0)
                .WithMessage("Lot price must be greater than 0");

            RuleFor(x => x.LotsCount)
                .GreaterThan(0)
                .WithMessage("Lots count must be greater than 0");
        }
    }
}
